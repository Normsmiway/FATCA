namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "ReportingFI", Namespace = "urn:oecd:ties:fatca:v2")]
    public class ReportingFI
    {
        [XmlElement(ElementName = "ResCountryCode", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string ResCountryCode { get; set; }
        [XmlElement(ElementName = "TIN", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string TIN { get; set; }
        [XmlElement(ElementName = "Name", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public Name Name { get; set; }
        [XmlElement(ElementName = "Address", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "FilerCategory", Namespace = "urn:oecd:ties:fatca:v2")]
        public string FilerCategory { get; set; }
        [XmlElement(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public DocSpec DocSpec { get; set; }
    }

}
