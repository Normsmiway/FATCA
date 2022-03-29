namespace Fatca.Core.Models
{
    /* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "PoolBalance", Namespace = "urn:oecd:ties:fatca:v2")]
    public class PoolBalance
    {
        [XmlAttribute(AttributeName = "currCode")]
        public string CurrCode { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}
