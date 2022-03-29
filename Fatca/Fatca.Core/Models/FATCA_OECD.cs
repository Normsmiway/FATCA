namespace Fatca.Core.Models
{
    using System.Collections.Generic;
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "FATCA_OECD", Namespace = "urn:oecd:ties:fatca:v2")]
    public class FATCA_OECD
    {
        [XmlElement(ElementName = "MessageSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public MessageSpec MessageSpec { get; set; }
        [XmlElement(ElementName = "FATCA", Namespace = "urn:oecd:ties:fatca:v2")]
        public FATCA FATCA { get; set; }
        [XmlAttribute(AttributeName = "iso", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Iso { get; set; }
        [XmlAttribute(AttributeName = "ftc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ftc { get; set; }
        [XmlAttribute(AttributeName = "stf", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Stf { get; set; }
        [XmlAttribute(AttributeName = "sfa", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sfa { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

}
