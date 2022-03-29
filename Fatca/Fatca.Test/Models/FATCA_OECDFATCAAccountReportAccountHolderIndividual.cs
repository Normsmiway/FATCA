namespace Fatca.Test.Old
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FATCA_OECDFATCAAccountReportAccountHolderIndividual
    {

        private string tINField;

        private Name nameField;

        private Address addressField;

        private BirthInfo birthInfoField;

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
       
        public BirthInfo BirthInfo
        {
            get
            {
                return this.birthInfoField;
            }
            set
            {
                this.birthInfoField = value;
            }
        }
    }


}

