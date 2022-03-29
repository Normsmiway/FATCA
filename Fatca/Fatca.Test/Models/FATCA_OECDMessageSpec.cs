using System.Xml.Serialization;

namespace Fatca.Test
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlRoot("ftc:FATCA_OECDMessageSpec")]
    public partial class FATCA_OECDMessageSpec
    {

        private string sendingCompanyINField;

        private string transmittingCountryField;

        private string receivingCountryField;

        private string messageTypeField;

        private string messageRefIdField;

        private System.DateTime reportingPeriodField;

        private System.DateTime timestampField;

        /// <remarks/>

        public string SendingCompanyIN
        {
            get
            {
                return this.sendingCompanyINField;
            }
            set
            {
                this.sendingCompanyINField = value;
            }
        }

        /// <remarks/>
       
        public string TransmittingCountry
        {
            get
            {
                return this.transmittingCountryField;
            }
            set
            {
                this.transmittingCountryField = value;
            }
        }

        /// <remarks/>
       
        public string ReceivingCountry
        {
            get
            {
                return this.receivingCountryField;
            }
            set
            {
                this.receivingCountryField = value;
            }
        }

        /// <remarks/>
       
        public string MessageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
            }
        }

        /// <remarks/>
     
        public string MessageRefId
        {
            get
            {
                return this.messageRefIdField;
            }
            set
            {
                this.messageRefIdField = value;
            }
        }

        /// <remarks/>

        public System.DateTime ReportingPeriod
        {
            get
            {
                return this.reportingPeriodField;
            }
            set
            {
                this.reportingPeriodField = value;
            }
        }

        /// <remarks/>
      
        public System.DateTime Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }
    }


}

