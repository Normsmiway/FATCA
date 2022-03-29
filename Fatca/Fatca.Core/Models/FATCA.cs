namespace Fatca.Core.Models
{
    /* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "FATCA", Namespace = "urn:oecd:ties:fatca:v2")]
    public class FATCA
    {
        [XmlElement(ElementName = "ReportingFI", Namespace = "urn:oecd:ties:fatca:v2")]
        public ReportingFI ReportingFI { get; set; }
        [XmlElement(ElementName = "ReportingGroup", Namespace = "urn:oecd:ties:fatca:v2")]
        public ReportingGroup ReportingGroup { get; set; }
    } 
}
