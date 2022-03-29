using Fatca.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Fatca.Core
{
    public class XmlFactory
    {
        public string Header { get; } = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            $" <ftc:FATCA_OECD xmlns:iso=\"urn:oecd:ties:isofatcatypes:v1\" xmlns:ftc=\"urn:oecd:ties:fatca:v2\"" +
            $" xmlns:stf=\"urn:oecd:ties:stf:v4\" xmlns:sfa=\"urn:oecd:ties:stffatcatypes:v2\"" +
            $" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" +
            $" xsi:schemaLocation=\"urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd\" version=\"2.0\">" +
               "</ftc:FATCA_OECD>";

        private readonly XDocument document;
        private readonly IConfiguration _config;
        private string _ftc;
        private string _sfa;
        private string path;
        public XmlFactory(IConfiguration configuration)
        {
            _config = configuration;
            //  Header = _config["FatcaSettings:header"] ?? throw new ArgumentNullException($"{nameof(Header)} cannot be null");
            document = XDocument.Parse(Header);
            _ftc = _config["FatcaSettings:ftcNS"] ?? "ftc";
            _sfa = _config["FatcaSettings:sfaNS"] ?? "sfa";
            path = _config["FatcaSettings:path"] ?? @"C:\FATCA";
        }
        public Task<bool> Create()
        {
            XElement FATCA_OECD = (XElement)document.FirstNode;
            XNamespace ftcNS = FATCA_OECD.GetNamespaceOfPrefix(_ftc);
            XNamespace sfaNS = FATCA_OECD.GetNamespaceOfPrefix(_sfa);

            XElement MessageSpec = new XElement(ftcNS + "MessageSpec", new object[] {
               new XElement(sfaNS + "SendingCompanyIN", "S519K4.99999.SL.392"),
               new XElement(sfaNS + "TransmittingCountry", "JP"),
               new XElement(sfaNS + "ReceivingCountry", "US"),
               new XElement(sfaNS + "MessageType", "FATCA"),
               new XElement(sfaNS + "MessageRefId", "DBA6455E-8454-47D9-914B-FEE48E4EF3AA"),
               new XElement(sfaNS + "ReportingPeriod", "2016-12-31"),
               new XElement(sfaNS + "Timestamp", "2017-01-17T09:30:47Z"),
            });

            FATCA_OECD.Add(MessageSpec);

            string fileName = Path.Combine(path, Path.GetRandomFileName().Replace('.', ' ').Trim() + ".xml");
            document.Save(fileName, SaveOptions.OmitDuplicateNamespaces);
            return Task.Run(() =>
            {

                //if (!File.Exists(path))
                //{
                //    // Create a file to write to.
                //    using (StreamWriter sw = File.CreateText(path))
                //    {
                //        sw.WriteLine(lineToLog);
                //    }
                //}
                return string.IsNullOrWhiteSpace("");
            });
        }

        public Task<bool> Create(string path = "")
        {
            var report = FatcaFactory.CreateFatca();
            path = Path.Combine(@"C:\FATCA", "AccessFileTestNew123.xml");
            return Task.Run(() =>
            {
                try
                {
                    using (StreamWriter fatcaXml = new StreamWriter(path, false))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(FATCA_OECD));
                        serializer.Serialize(fatcaXml, report);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });

        }


    }
}