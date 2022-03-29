namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "AddressFix", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class AddressFix
    {
        [XmlElement(ElementName = "Street", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string Street { get; set; }
        [XmlElement(ElementName = "BuildingIdentifier", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string BuildingIdentifier { get; set; }
        [XmlElement(ElementName = "SuiteIdentifier", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string SuiteIdentifier { get; set; }
        [XmlElement(ElementName = "FloorIdentifier", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string FloorIdentifier { get; set; }
        [XmlElement(ElementName = "DistrictName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string DistrictName { get; set; }
        [XmlElement(ElementName = "POB", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string POB { get; set; }
        [XmlElement(ElementName = "PostCode", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string PostCode { get; set; }
        [XmlElement(ElementName = "City", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string City { get; set; }
        [XmlElement(ElementName = "CountrySubentity", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string CountrySubentity { get; set; }
    }

}
