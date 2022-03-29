namespace Fatca.Core
{

    //Global/Namespace Settings
    public class GlobalSettings
    {
        public string Ftc { get; set; } = "urn:oecd:ties:fatca:v2";
        public string Version { get; set; } = "2.0";
        public string Iso { get; set; } = "urn:oecd:ties:isofatcatypes:v1";
        public string SchemaLocation { get; set; } = "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd";
        public string Sfa { get; set; } = "urn:oecd:ties:stffatcatypes:v2";
        public string Stf { get; set; } = "urn:oecd:ties:stf:v4";
    }


}