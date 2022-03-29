using System.Xml.Schema;

namespace Fatca.Test.Old
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oecd:ties:fatca:v2")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd", IsNullable = false)]
   
    //xmlns:iso="urn:oecd:ties:isofatcatypes:v1" 
    //xmlns:ftc="urn:oecd:ties:fatca:v2"
    //xmlns:stf="urn:oecd:ties:stf:v4" 
    //xmlns:sfa="urn:oecd:ties:stffatcatypes:v2"
    public partial class FATCA_OECD
    {

        private FATCA_OECDMessageSpec messageSpecField;

        private FATCA_OECDFATCA fATCAField;

        private decimal versionField;

        /// <remarks/>
        public FATCA_OECDMessageSpec MessageSpec
        {
            get
            {
                return this.messageSpecField;
            }
            set
            {
                this.messageSpecField = value;
            }
        }

        /// <remarks/>
        public FATCA_OECDFATCA FATCA
        {
            get
            {
                return this.fATCAField;
            }
            set
            {
                this.fATCAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }


}

