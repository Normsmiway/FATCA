namespace Fatca.Core.Models
{
    using System;
    using System.Net.Http.Headers;
    /* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "MessageSpec", Namespace = "urn:oecd:ties:fatca:v2")]
    public class MessageSpec
    {
        [XmlElement(ElementName = "SendingCompanyIN", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string SendingCompanyIN { get; set; }
        [XmlElement(ElementName = "TransmittingCountry", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string TransmittingCountry { get; set; }
        [XmlElement(ElementName = "ReceivingCountry", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string ReceivingCountry { get; set; }
        [XmlElement(ElementName = "MessageType", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string MessageType { get; set; }
        [XmlElement(ElementName = "MessageRefId", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string MessageRefId { get; set; }
        [XmlElement(ElementName = "ReportingPeriod", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string ReportingPeriod { get; set; }
        [XmlElement(ElementName = "Timestamp", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string Timestamp { get; set; }
    }


    
}
