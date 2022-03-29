using Fatca.Core.Models;

namespace Fatca.Core.Builders.Fatcas
{
    public class ReportingFIBuilder<T> : FatcaBuilder where T : ReportingFIBuilder<T>
    {

        public T WithResCountryCode(string countryCode)
        {
            _fatca.ReportingFI.ResCountryCode = countryCode;
            return ApplyChanges();
        }
        public T AndTaxIndentificationNumber(string TIN)
        {
            _fatca.ReportingFI.TIN = TIN;
            return ApplyChanges();
        }

        public T WithFilerCategory(string filerCategory)
        {
            _fatca.ReportingFI.FilerCategory = filerCategory;
            return ApplyChanges();
        }

        private T ApplyChanges() => (T)this;
    }
}
