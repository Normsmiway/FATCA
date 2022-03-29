namespace Fatca.Test
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FATCA_OECDFATCAReportingFIDocSpec
    {

        private string docTypeIndicField;

        private string docRefIdField;

        /// <remarks/>
        public string DocTypeIndic
        {
            get
            {
                return this.docTypeIndicField;
            }
            set
            {
                this.docTypeIndicField = value;
            }
        }

        /// <remarks/>
        public string DocRefId
        {
            get
            {
                return this.docRefIdField;
            }
            set
            {
                this.docRefIdField = value;
            }
        }
    }


}

