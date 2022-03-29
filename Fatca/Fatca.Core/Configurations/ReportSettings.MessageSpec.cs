namespace Fatca.Core
{
    public class MessageSpecSettings
    {
        //MessageSpec
        public string TransmittingCountry { get; set; } = "NG";
        public string MessageRefId { get; set; } //Internally generated
        public string MessageType { get; set; } = "FATCA";
        public string ReceivingCountry { get; set; } = "US";
        public string ReportPeriod { get; set; } //User Input
        public string SendingCompanyIN { get; set; } = "";
        public string TIN { get; set; }
    }


}