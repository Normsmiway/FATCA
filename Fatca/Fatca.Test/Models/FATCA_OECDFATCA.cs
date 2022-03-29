namespace Fatca.Test.Old
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oecd:ties:fatca:v2")]
    public partial class FATCA_OECDFATCA
    {

        private FATCA_OECDFATCAReportingFI reportingFIField;

        private FATCA_OECDFATCAAccountReport[] reportingGroupField;

        /// <remarks/>
        public FATCA_OECDFATCAReportingFI ReportingFI
        {
            get
            {
                return this.reportingFIField;
            }
            set
            {
                this.reportingFIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AccountReport", IsNullable = false)]
        public FATCA_OECDFATCAAccountReport[] ReportingGroup
        {
            get
            {
                return this.reportingGroupField;
            }
            set
            {
                this.reportingGroupField = value;
            }
        }
    }


}

