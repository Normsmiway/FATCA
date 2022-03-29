namespace Fatca.Test.Old
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Address
    {

        private string countryCodeField;

        private AddressAddressFix addressFixField;

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        public AddressAddressFix AddressFix
        {
            get
            {
                return this.addressFixField;
            }
            set
            {
                this.addressFixField = value;
            }
        }
    }


}

