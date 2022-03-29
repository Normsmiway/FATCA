namespace Fatca.Core.Models
{
    /* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "PoolReport", Namespace = "urn:oecd:ties:fatca:v2")]
    public class PoolReport
    {
        [XmlElement(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public DocSpec DocSpec { get; set; }
        [XmlElement(ElementName = "AccountCount", Namespace = "urn:oecd:ties:fatca:v2")]
        public string AccountCount { get; set; }
        [XmlElement(ElementName = "AccountPoolReportType", Namespace = "urn:oecd:ties:fatca:v2")]
        public string AccountPoolReportType { get; set; }
        [XmlElement(ElementName = "PoolBalance", Namespace = "urn:oecd:ties:fatca:v2")]
        public PoolBalance PoolBalance { get; set; }
    }
}
