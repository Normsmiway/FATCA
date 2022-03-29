namespace Fatca.Core.Models
{
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Name", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class Name
    {
        private string[] textField;
        [XmlElement(ElementName = "FirstName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "MiddleName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string MiddleName { get; set; }
        [XmlElement(ElementName = "LastName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string LastName { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

}
