namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "AccountReport", Namespace = "urn:oecd:ties:fatca:v2")]
    public class AccountReport
    {
        [XmlElement(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public DocSpec DocSpec { get; set; }
        [XmlElement(ElementName = "AccountNumber", Namespace = "urn:oecd:ties:fatca:v2")]
        public string AccountNumber { get; set; }
        [XmlElement(ElementName = "AccountHolder", Namespace = "urn:oecd:ties:fatca:v2")]
        public AccountHolder AccountHolder { get; set; }
        [XmlElement(ElementName = "AccountBalance", Namespace = "urn:oecd:ties:fatca:v2")]
        public AccountBalance AccountBalance { get; set; }
    }

}
