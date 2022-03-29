namespace Fatca.Test.Old
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FATCA_OECDFATCAAccountReport
    {

        private FATCA_OECDFATCAAccountReportDocSpec docSpecField;

        private uint accountNumberField;

        private FATCA_OECDFATCAAccountReportAccountHolder accountHolderField;

        private FATCA_OECDFATCAAccountReportAccountBalance accountBalanceField;

        /// <remarks/>
        public FATCA_OECDFATCAAccountReportDocSpec DocSpec
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

        /// <remarks/>
        public uint AccountNumber
        {
            get
            {
                return this.accountNumberField;
            }
            set
            {
                this.accountNumberField = value;
            }
        }

        /// <remarks/>
        public FATCA_OECDFATCAAccountReportAccountHolder AccountHolder
        {
            get
            {
                return this.accountHolderField;
            }
            set
            {
                this.accountHolderField = value;
            }
        }

        /// <remarks/>
        public FATCA_OECDFATCAAccountReportAccountBalance AccountBalance
        {
            get
            {
                return this.accountBalanceField;
            }
            set
            {
                this.accountBalanceField = value;
            }
        }
    }


}

