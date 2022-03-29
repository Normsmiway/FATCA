namespace Fatca.Test.Old
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FATCA_OECDFATCAReportingFI
    {

        private string resCountryCodeField;

        private string tINField;

        private Name nameField;

        private Address addressField;

        private string filerCategoryField;

        private FATCA_OECDFATCAReportingFIDocSpec docSpecField;

        /// <remarks/>
     
        public string ResCountryCode
        {
            get
            {
                return this.resCountryCodeField;
            }
            set
            {
                this.resCountryCodeField = value;
            }
        }

        /// <remarks/>
     
        public string TIN
        {
            get
            {
                return this.tINField;
            }
            set
            {
                this.tINField = value;
            }
        }

        /// <remarks/>
     
        public Name Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
    
        public Address Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public string FilerCategory
        {
            get
            {
                return this.filerCategoryField;
            }
            set
            {
                this.filerCategoryField = value;
            }
        }

        /// <remarks/>
        public FATCA_OECDFATCAReportingFIDocSpec DocSpec
        {
            get
            {
                return this.docSpecField;
            }
            set
            {
                this.docSpecField = value;
            }
        }
    }


}

