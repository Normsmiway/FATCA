namespace Fatca.Core.Models
{
    using System.Collections.Generic;
    /* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "ReportingGroup", Namespace = "urn:oecd:ties:fatca:v2")]
    public class ReportingGroup
    {
        [XmlElement(ElementName = "AccountReport", Namespace = "urn:oecd:ties:fatca:v2")]
        public List<AccountReport> AccountReports { get; set; }

        [XmlElement(ElementName = "PoolReport", Namespace = "urn:oecd:ties:fatca:v2")]
        public PoolReport PoolReport { get; set; }
    }

   
}
