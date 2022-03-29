namespace Fatca.Core
{
    public partial class ReportSettings
    {
        public GlobalSettings Global { get; set; }
        public MessageSpecSettings MessageSpec { get; set; }
        public FIAddressSettings FIAddress { get; set; }
        public string FilerCategory { get; set; }
        public string FilePath { get; set; } = "C:\\Users\\Surface\\Documents\\Clients\\Access\\FATCA\\App\\Fatca\\Fatca.Test\\TestFiles";
    }
}