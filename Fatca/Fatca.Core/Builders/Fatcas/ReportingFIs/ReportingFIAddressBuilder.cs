using Fatca.Core.Models;

namespace Fatca.Core.Builders.Fatcas
{
    public class ReportingFIAddressBuilder<T> : ReportingFINameBuilder<ReportingFIAddressBuilder<T>> where T : ReportingFIAddressBuilder<T>
    {
        private Address _address;
       
        public T AddAddress()
        {
            _address = new Address();
            return ApplyChanges();
        }
        public  T WithCountryCode(string countryCode)
        {
            _address.CountryCode = countryCode;
            return ApplyChanges();

        }

        private T ApplyChanges()
        {

            _fatca.ReportingFI.Address = _address;
            return (T)this;
        }
    }
}
