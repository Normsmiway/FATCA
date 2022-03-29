using Fatca.Core.Models;

namespace Fatca.Core.Builders.Fatcas
{
    public class ReportingFIAddressFixBuilder<T> : ReportingFIAddressBuilder<ReportingFIAddressFixBuilder<T>> where T : ReportingFIAddressFixBuilder<T>
    {
        private AddressFix _addressFix;
        public T IncludingAddressFix()
        {
            _addressFix = new AddressFix();
            return ApplyChanges();
        }

        public T At(string street)
        {
            _addressFix.Street = street;
            return ApplyChanges();
        }
        public T WithBuildingIdentifier(string buildingIdentifier)
        {
            _addressFix.BuildingIdentifier = buildingIdentifier;
            return ApplyChanges();
        }
        public T AndSuiteIdentifier(string suiteIdentifier)
        {
            _addressFix.SuiteIdentifier = suiteIdentifier;
            return ApplyChanges();
        }

        public T WithFloorIdentifier(string floorIdentifier = "N/A")
        {
            _addressFix.FloorIdentifier = floorIdentifier;
            return ApplyChanges();
        }
        public T WithDistrictName(string districtName = "N/A")
        {
            _addressFix.DistrictName = districtName;
            return ApplyChanges();
        }
        public T AndPOB(string POB = "N/A")
        {
            _addressFix.POB = POB;
            return ApplyChanges();
        }

        public T WithPostCode(string postCode = "N/A")
        {
            _addressFix.PostCode = postCode;
            return ApplyChanges();
        }

        public T In(string city = "N/A")
        {
            _addressFix.City = city;
            return ApplyChanges();
        }

        public T WithCountrySubentity(string countrySubentity)
        {
            _addressFix.CountrySubentity = countrySubentity;
            return ApplyChanges();
        }
        private T ApplyChanges()
        {
            _fatca.ReportingFI.Address.AddressFix = _addressFix;
            return (T)this;
        }


    }
}
