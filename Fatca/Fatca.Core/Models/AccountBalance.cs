namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "AccountBalance", Namespace = "urn:oecd:ties:fatca:v2")]
    public class AccountBalance
    {
        [XmlAttribute(AttributeName = "currCode")]
        public string CurrCode { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}
