using System.IO;

namespace Fatca.Core.Configurations
{
    public class FatcaSettings
    {
        // private const string path = @"C:\Users\Surface\Documents\Clients\Access\FATCA\App\Fatca\Fatca.Test\TestFiles";
        public string FilePath { get; set; } = @"C:\Users\Surface\Documents\Clients\Access\FATCA\App\Fatca\Fatca.Test\TestFiles";
        public string SenderCertificateName { get; set; } = "sender.p12";
        public string ReceiverCertificateName { get; set; }= "receiver.p12";
        public string Certificate { get { return Path.Combine(FilePath, SenderCertificateName); } }// =$@"{RootPath}\sender.p12";
        public string SchemaFolder { get { return Path.Combine(FilePath, "schema"); } }// = $@"{path}\schema";
        public string ReceiverPublicKey { get { return Path.Combine(FilePath, ReceiverCertificateName); } }// = $@"{path}\receiver.p12";
        public string Password { get; set; } = "password";
        public string ReceiverPublicKeyPassword { get; set; } = "";

        public bool UseECBMode { get; set; } = false;
        public bool ValidateSignature { get; set; } = false;
        public bool ValidateSchema { get; set; } = true;
        public bool UseModel1Option2 { get; set; } = false;
        public string HctaEncryptionKey { get; set; } = "";
        public string HctaEncryptionKeyPassword { get; set; }
        public string HctaCode { get; set; } = "";
        //SFTP setting
        public bool AutoSendSFTP { get; set; } = false;
        public string SftpUserName { get; set; } = string.Empty;
        public string SftpPassword { get; set; } = string.Empty;
        public string SftpRemoteServer { get; set; } = string.Empty;
        public bool SendEntireFolder { get; set; } = true;
        public int Delay { get; set; } = 1000;
    }
}
