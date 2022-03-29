namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
    public class DocSpec
    {
        [XmlElement(ElementName = "DocTypeIndic", Namespace = "urn:oecd:ties:fatca:v2")]
        public string DocTypeIndic { get; set; }
        [XmlElement(ElementName = "DocRefId", Namespace = "urn:oecd:ties:fatca:v2")]
        public string DocRefId { get; set; }
    }

}
