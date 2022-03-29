namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Address", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class Address
    {
        [XmlElement(ElementName = "CountryCode", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string CountryCode { get; set; }
        [XmlElement(ElementName = "AddressFix", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public AddressFix AddressFix { get; set; }
    }

}
