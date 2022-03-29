using Fatca.Core.Builders.Fatcas.ReportingGroups;
using Fatca.Core.Models;

namespace Fatca.Core.Builders
{
    public abstract class FatcaBuilder
    {
        protected FATCA _fatca;
        public FatcaBuilder()
        {
            _fatca = new FATCA();
        }
        public FATCA Build() => _fatca;
    }

    public class Create : AccountBalanceBuilder<Create>
    {
        public static ReportingFI reportingFI;
        public Create()
        {
            _fatca.ReportingFI = new ReportingFI();

        }

        public static Create ReportingFinanciaIInstitution => new Create();
    }
}
