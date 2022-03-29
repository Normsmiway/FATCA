namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "AccountHolder", Namespace = "urn:oecd:ties:fatca:v2")]
    public class AccountHolder
    {
        [XmlElement(ElementName = "Individual", Namespace = "urn:oecd:ties:fatca:v2")]
        public Individual Individual { get; set; }
    }

}
