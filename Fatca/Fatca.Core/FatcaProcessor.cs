using Fatca.Core.Configurations;
using Fatca.Core.Services;
using Fatca.Core.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WinSCP;

namespace Fatca.Core
{
    public class FatcaProcessor : IValidate, ISign, IEncrypt//: IFatcaProcessor
    {
        public IDictionary<string, string> ValidationErrors { get; set; }
        public IDictionary<string, string> SuccessMessages { get; set; }
        public bool HasError => ValidationErrors.Any();
        private readonly FatcaSettings _settings;
        public ProcessingService fileProcessingService = null;
        readonly ILogger<FatcaProcessor> _logger;
        public FatcaProcessor(FatcaSettings settings, ILogger<FatcaProcessor> logger)
        {
            _settings = settings;
            _logger = logger;
            ValidationErrors = new Dictionary<string, string>();
            SuccessMessages = new Dictionary<string, string>();

            fileProcessingService = new ProcessingService(); //Publisher
            var subscriber = new ProcessSubscriber();
            fileProcessingService.FileProcessed += subscriber.OnFileCompressed;
            //  fileProcessingService.FileProcessed += OnFileCompressed;

        }
        private FatcaProcessor()
        {
            _settings = new FatcaSettings();
            ValidationErrors = new Dictionary<string, string>();
            SuccessMessages = new Dictionary<string, string>();
        }

        public static FatcaProcessor Create() => new FatcaProcessor();
        public ISign Validate()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            // Console.Write("Validated");
            return this;
        }
        /// <summary>
        /// Sign Fatca xml file
        /// </summary>
        /// <returns></returns>
        public IEncrypt Sign(bool loadFromFolder = false, string path = "")
        {
            fileProcessingService.Process(new ProcessedFile() { FileName = "File" }, "processing started");
            string processedFolder = string.Empty;
            try
            {
                string[] filesToProcess;
                if (loadFromFolder)
                {
                    // Load all XML files in the folder into an array
                    filesToProcess = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);
                    fileProcessingService.Process(new ProcessedFile() { FileName = $"{filesToProcess?.Count()} File" }, "found");
                    processedFolder = Path.Combine(path, "Processed");

                    //if the processedFolder doesn't exist, create it
                    if (!Directory.Exists(processedFolder))
                    {
                        Directory.CreateDirectory(processedFolder);
                    }
                }
                else
                {
                    filesToProcess = new string[1];
                    filesToProcess[0] = path;
                }

                string currentFileToProcess = "";
                foreach (var fileName in filesToProcess)
                {
                    currentFileToProcess = fileName;
                    //if the file we are processing has an underscore we will 
                    //split it off for logging
                    //this should only happen for bulk sending from a folder
                    if (currentFileToProcess.Contains("_"))
                    {
                        string[] filePart = fileName.Split('_');
                        currentFileToProcess = Path.Combine(path, filePart[1]);
                        File.Move(fileName, currentFileToProcess);
                    }

                    // perform the schema validation if we have the checkbox checked for it
                    if (_settings.ValidateSchema)
                    {
                        fileProcessingService.Process(new ProcessedFile() { FileName = "" }, "Validating...");
                        string validationError = XmlManager.CheckSchema(currentFileToProcess, _settings.SchemaFolder);
                        if (validationError != "")
                        {
                            if (!ValidationErrors.ContainsKey("SchemaError"))
                                ValidationErrors.Add("SchemaError", "Schema Validation Error:\r\n" + validationError);
                            //terminate process if shcema validation fails
                            break;
                        }
                        fileProcessingService.Process(new ProcessedFile() { FileName = "" }, "Validated");
                    }

                    // load XML file content
                    byte[] xmlContent = File.ReadAllBytes(currentFileToProcess);
                    string senderGIIN = Path.GetFileNameWithoutExtension(currentFileToProcess);
                    string filePath = Path.GetDirectoryName(currentFileToProcess);
                    string fileExtension = Path.GetExtension(currentFileToProcess.ToUpper()).Replace(".", "");
                    bool isXML = true;

                    string UsedFIlesOutBoxFilePath = Path.Combine(filePath, "OutBox", senderGIIN);
                    string zipOutBoxFilePath = Path.Combine(filePath, "OutBox", senderGIIN);

                    if (!Directory.Exists(UsedFIlesOutBoxFilePath)) { Directory.CreateDirectory(UsedFIlesOutBoxFilePath); }


                    if (fileExtension != "XML")
                    {
                        isXML = false;
                    }

                    // perform signature
                    byte[] envelopingSignature;
                    string envelopingFileName = "";

                    //if the file is XML we will use the enveloping digital signature for the XML
                    if (isXML == true)
                    {
                        string name = Path.Combine(UsedFIlesOutBoxFilePath, Path.GetFileName(currentFileToProcess));
                        fileProcessingService.Process(new ProcessedFile() { FileName = name }, "signing...");
                        envelopingSignature = XmlManager.Sign(XmlSignatureType.Enveloping, xmlContent, _settings.Certificate, _settings.Password, filePath);
                        //string envelopingFilePath = Path.Combine(filePath, "OutBox");
                        envelopingFileName = name.Replace(".xml", "_Payload.xml");
                        fileProcessingService.Process(new ProcessedFile() { FileName = name }, " signed");

                    }
                    //if the file is NOT XML, we will convert the file data to base64 and put in XML and sign it
                    else
                    {
                        string name = Path.Combine(UsedFIlesOutBoxFilePath, Path.GetFileName(currentFileToProcess));
                        fileProcessingService.Process(new ProcessedFile() { FileName = name }, " signing...");
                        envelopingSignature = XmlManager.Sign(XmlSignatureType.NonXML, xmlContent, _settings.Certificate, _settings.Password, filePath);
                        //  string envelopingFilePath = Path.Combine(filePath, "OutBox");
                        envelopingFileName = name.ToUpper().Replace(".PDF", "_Payload.xml");
                        fileProcessingService.Process(new ProcessedFile() { FileName = name }, " signinged");

                    }

                    string zipFileName = envelopingFileName.Replace(".xml", ".zip");


                    // File.WriteAllBytes(envelopingFileName.GetDatePathBaseOnCurrentPath(), envelopingSignature);

                    // save enveloping version to disk
                    File.WriteAllBytes(envelopingFileName, envelopingSignature);

                    // ZipManager.CreateArchive(envelopingFileName.GetDatePathBaseOnCurrentPath(), zipFileName);
                    // add enveloping signature to ZIP file
                    fileProcessingService.Process(new ProcessedFile() { FileName = envelopingFileName }, " compressing");
                    ZipManager.CreateArchive(envelopingFileName, zipFileName);
                    fileProcessingService.Process(new ProcessedFile() { FileName = envelopingFileName }, " Completed");
                    // generate AES key (32 bytes) & default initialization vector (empty)
                    byte[] aesEncryptionKey = AesManager.GenerateRandomKey(AesManager.KeySize / 8);
                    byte[] aesEncryptionVector = AesManager.GenerateRandomKey(16, false);


                    // encrypt file & save to disk
                    string encryptedFileName = zipFileName.Replace(".zip", "");
                    string encryptedHCTAFileName = zipFileName.Replace(".zip", "");
                    string payloadFileName = encryptedFileName + "";
                    AesManager.EncryptFile(zipFileName, encryptedFileName, aesEncryptionKey, aesEncryptionVector, useECBMode: _settings.UseECBMode);

                    fileProcessingService.Process(new ProcessedFile() { FileName = envelopingFileName }, " encrypting...");
                    // encrypt key with public key of certificate & save to disk
                    string keyName = Path.GetFileNameWithoutExtension(fileName);
                    encryptedFileName = Path.GetDirectoryName(zipFileName) + $"\\{keyName}.TA.840_Key"; //"\\000000.00000.TA.840_Key";
                    AesManager.EncryptAesKey(aesEncryptionKey, aesEncryptionVector, _settings.ReceiverPublicKey, _settings.Password, encryptedFileName, useECBMode: _settings.UseECBMode);
                    //For Model1 Option2 Only, encrypt the AES Key with the HCTA Public Key
                    if (_settings.UseModel1Option2)
                    {
                        encryptedHCTAFileName = Path.GetDirectoryName(zipFileName) + $"\\{keyName}.TA." + _settings.HctaCode + "_Key";
                        AesManager.EncryptAesKey(aesEncryptionKey, aesEncryptionVector, _settings.HctaEncryptionKey,
                            _settings.HctaEncryptionKeyPassword, encryptedHCTAFileName, useECBMode: _settings.UseECBMode);
                    }

                    // cleanup
                    envelopingSignature = null;
                    aesEncryptionKey = aesEncryptionVector = null;

                    try
                    {
                        DateTime uDat = new DateTime();
                        uDat = DateTime.UtcNow;
                        string senderFile = uDat.ToString("yyyyMMddTHHmmssfffZ") + "_" + senderGIIN;
                        string metadataFileName = Path.Combine(UsedFIlesOutBoxFilePath, $"{senderGIIN}_Metadata.xml");
                        XmlManager.CreateMetadataFile(metadataFileName, fileExtension, isXML, taxYear: DateTime.Now.Year.ToString(), senderGIIN, senderFile);

                        //Check the signature to make sure it is valid, this requires the KeyInfo to be present
                        //This is controlled using the checkbox on the form
                        //This should be commented out or not selected if not using the KeyInfo in the XmlManager class
                        if (_settings.ValidateSignature)
                        {
                            //  bool result0 = XmlManager.CheckSignature(envelopingFileName.GetDatePathBaseOnCurrentPath());
                            bool result = XmlManager.CheckSignature(envelopingFileName);
                            if (!result)
                            {
                                if (!ValidationErrors.ContainsKey("SignatureValidationError"))
                                    ValidationErrors.Add("SignatureValidationError", "Signature is not valid!");
                                //terminate processing if signature validation fails
                                break;
                            }
                        }
                        fileProcessingService.Process(new ProcessedFile() { FileName = envelopingFileName }, " encrypted");
                        //Add the metadata, payload, and key files to the final zip package
                        // add enveloping signature to ZIP file

                        fileProcessingService.Process(new ProcessedFile() { FileName = senderFile }, " compressing");
                        ZipManager.CreateArchive(metadataFileName, Path.Combine(zipOutBoxFilePath, $"{senderFile}.zip"));
                        ZipManager.UpdateArchive(encryptedFileName, Path.Combine(zipOutBoxFilePath, $"{senderFile}.zip"));
                        ZipManager.UpdateArchive(payloadFileName, Path.Combine(zipOutBoxFilePath, $"{senderFile}.zip"));
                        fileProcessingService.Process(new ProcessedFile() { FileName = senderFile }, " Completed");

                        //Add the HCTA Key file for a M1O2 packet
                        if (_settings.UseModel1Option2)
                        {
                            ZipManager.UpdateArchive(encryptedHCTAFileName, Path.Combine(UsedFIlesOutBoxFilePath, $"{senderFile}.zip"));
                        }



                        if (_settings.AutoSendSFTP)
                        {
                            string sftpUpName = Path.Combine(UsedFIlesOutBoxFilePath, $"{senderFile}.zip");
                            fileProcessingService.Process(new ProcessedFile() { FileName = senderFile }, "Uploading");
                            SessionOptions currentSFTPSession = SFTPManager.CreateSFTPSession(_settings.SftpRemoteServer, _settings.SftpUserName, _settings.SftpPassword);
                            string transferResult = SFTPManager.UploadFile(currentSFTPSession, sftpUpName);
                            //This can be commented out if there is no desire to see an upload confirmation
                            fileProcessingService.Process(new ProcessedFile() { FileName = senderFile }, transferResult);
                            SuccessMessages.Add("SftpUploadResult", transferResult);

                            //write to log
                            string logPath = @"simplelog.csv";
                            // This text is added only once to the file.
                            string lineToLog = fileName + "," + currentFileToProcess + "," + senderFile + ",IDESTRANSID,NOTIFICATIONID,NULL,ERRORCOUNT";
                            if (!File.Exists(logPath))
                            {
                                // Create a file to write to.
                                using (StreamWriter sw = File.CreateText(logPath))
                                {
                                    sw.WriteLine(lineToLog);
                                    _logger.LogInformation(lineToLog);
                                }
                            }
                            else
                            {
                                // This text is always added, making the file longer over time
                                // if it is not deleted.
                                using (StreamWriter sw = File.AppendText(logPath))
                                {
                                    sw.WriteLine(lineToLog);
                                    _logger.LogInformation(lineToLog);
                                }
                            }
                        }
                        if (_settings.SendEntireFolder)
                        {
                            _logger.LogInformation("MOVING FILES...");
                            var zipped = Path.Combine(filePath, "OutBox\\Zipped");
                            if (!Directory.Exists(zipped))
                            {
                                Directory.CreateDirectory(zipped);
                            }
                            //Delay for one second
                            Task.Delay(_settings.Delay);
                            //Move the file to a processed folder so we can move on to the next
                            //This is only used when sending an entire folder
                            string currentFilePath = Path.Combine(processedFolder, Path.GetFileName(fileName));
                            //  File.Move(currentFileToProcess, processedFolder + "\\" + Path.GetFileName(fileName));
                            string zippedPath = Path.Combine(zipped, $"{senderFile}.zip");
                            File.Move(currentFileToProcess, currentFilePath);
                            File.Move(Path.Combine(zipOutBoxFilePath, $"{senderFile}.zip"), Path.Combine(filePath, zippedPath));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, ex);
                        if (!ValidationErrors.ContainsKey("XmlProcessingError"))
                            ValidationErrors.Add("XmlProcessingError", ex.Message);
                    }
                    finally
                    {

                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" =>Signed");
                return this;
            }
            catch (Exception ex)
            {
                if (!ValidationErrors.ContainsKey("SigningError"))
                    ValidationErrors.Add("SigningError", ex.Message);
                Console.WriteLine($"Error: {ex.StackTrace}");
                _logger.LogError(ex.Message, ex);
                return this;
            }

        }
        public FatcaProcessor Excypt()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" =>Encrypted");
            return this;
        }

        public void Apply()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" =>Completed");
            Console.ResetColor();
        }

    }
}
