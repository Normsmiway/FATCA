namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "BirthInfo", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class BirthInfo
    {
        [XmlElement(ElementName = "BirthDate", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string BirthDate { get; set; }
    }

}
