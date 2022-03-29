using Fatca.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Fatca.Core.Builders.Fatcas.ReportingGroups
{
    public class AccountReportsBuilder<T> : ReportingGroupBuilder<AccountReportsBuilder<T>> where T : AccountReportsBuilder<T>
    {
        public List<AccountReport> _accountReports;

        public AccountReport AccountReport { get; } = new AccountReport();
        public T AddAccountReports()
        {
            if (_accountReports is null)
            {
                _accountReports = new List<AccountReport>();
            }

            return ApplyChanges();
        }

        public T WithAccountNumber(string accountNumber)
        {
            AccountReport.AccountNumber = accountNumber;

            return ApplyChanges();
        }
        private T ApplyChanges()
        {
            _fatca.ReportingGroup.AccountReports = _accountReports;
            return (T)this;
        }
    }

    public class ReportingGroupDocSpecBuilder<T> : AccountReportsBuilder<ReportingGroupDocSpecBuilder<T>> where T : ReportingGroupDocSpecBuilder<T>
    {
        private DocSpec _docSpec;

        public T AddDocSpec()
        {
            _docSpec = new DocSpec();
            return ApplyChanges();
        }

        private T ApplyChanges()
        {
            AccountReport.DocSpec = _docSpec;
            return (T)this;
        }

        public T WithDocTypeIndic(string docTypeIndic)
        {
            _docSpec.DocTypeIndic = docTypeIndic;
            return ApplyChanges();
        }

        public new T WithDocRefId(string docRefId)
        {
            _docSpec.DocRefId = docRefId;
            return ApplyChanges();
        }
    }
    public class AccountHolderBuilder<T> : ReportingGroupDocSpecBuilder<AccountHolderBuilder<T>> where T : AccountHolderBuilder<T>
    {
        private AccountHolder _accountHolder;
        public T AddACoountHolder()
        {
            _accountHolder = new AccountHolder();
            return ApplyChanges();
        }

        private T ApplyChanges()
        {
            AccountReport.AccountHolder = _accountHolder;
            return (T)this;
        }
    }
    public class IndividualBuilder<T> : AccountHolderBuilder<IndividualBuilder<T>> where T : IndividualBuilder<T>
    {
        public Individual _individual;
        public T AddIndividual()
        {
            _individual = new Individual();
            return ApplyChanges();
        }

        public T WithIndividualTIN(string TIN)
        {
            _individual.TIN = TIN;
            return ApplyChanges();
        }

        private T ApplyChanges()
        {
            AccountReport.AccountHolder.Individual = _individual;
            return (T)this;
        }
    }
    public class ReportingGroupNameBuilder<T> : IndividualBuilder<ReportingGroupNameBuilder<T>> where T : ReportingGroupNameBuilder<T>
    {

        private Name _name;

        public T AddName()
        {
            _name = new Name();
            return (T)this;
        }

        public T WithFirstName(string firstname)
        {
            //  _fatca.ReportingFI.Name.FirstName = firstname;
            _name.FirstName = firstname;
            return ApplyChanges();

        }
        public T WithMiddleName(string middleName)
        {
            _name.MiddleName = middleName;
            return ApplyChanges();

        }
        public T WithLastName(string lastName)
        {
            _name.LastName = lastName;
            return ApplyChanges();

        }

        public new T WithParams(string[] param)
        {
            _name.Text = param;
            return ApplyChanges();

        }

        private T ApplyChanges()
        {
            AccountReport.AccountHolder.Individual.Name = _name;
            return (T)this;
        }
    }
    public class ReportingGroupAddressBuilder<T> : ReportingGroupNameBuilder<ReportingGroupAddressBuilder<T>> where T : ReportingGroupAddressBuilder<T>
    {
        private Address _address;

        public new T AddAddress()
        {
            _address = new Address();
            return ApplyChanges();
        }
        public new T WithCountryCode(string countryCode)
        {
            _address.CountryCode = countryCode;
            return ApplyChanges();

        }

        private T ApplyChanges()
        {

            AccountReport.AccountHolder.Individual.Address = _address;
            return (T)this;
        }
    }
    public class ReportingGroupAddressFixBuilder<T> : ReportingGroupAddressBuilder<ReportingGroupAddressFixBuilder<T>> where T : ReportingGroupAddressFixBuilder<T>
    {
        private AddressFix _addressFix;
        public T AddAddressFix()
        {
            _addressFix = new AddressFix();
            return ApplyChanges();
        }

        public T WithStreet(string street)
        {
            _addressFix.Street = street;
            return ApplyChanges();
        }
        public new T WithBuildingIdentifier(string buildingIdentifier)
        {
            _addressFix.BuildingIdentifier = buildingIdentifier;
            return ApplyChanges();
        }
        public T WithSuiteIdentifier(string suiteIdentifier)
        {
            _addressFix.SuiteIdentifier = suiteIdentifier;
            return ApplyChanges();
        }

        public new T WithFloorIdentifier(string floorIdentifier = "N/A")
        {
            _addressFix.FloorIdentifier = floorIdentifier;
            return ApplyChanges();
        }
        public new T WithDistrictName(string districtName = "N/A")
        {
            _addressFix.DistrictName = districtName;
            return ApplyChanges();
        }
        public T WithPOB(string POB = "N/A")
        {
            _addressFix.POB = POB;
            return ApplyChanges();
        }

        public new T WithPostCode(string postCode = "N/A")
        {
            _addressFix.PostCode = postCode;
            return ApplyChanges();
        }

        public new T WithCity(string city = "N/A")
        {
            _addressFix.City = city;
            return ApplyChanges();
        }

        public new T WithCountrySubentity(string countrySubentity)
        {
            _addressFix.CountrySubentity = countrySubentity;
            return ApplyChanges();
        }
        private T ApplyChanges()
        {
            AccountReport.AccountHolder.Individual.Address.AddressFix = _addressFix;
            return (T)this;
        }

    }
    public class BirthInfoBuilder<T> : ReportingGroupAddressFixBuilder<BirthInfoBuilder<T>> where T : BirthInfoBuilder<T>
    {
        private BirthInfo _birthInfo;
        public T AddBirthInfo()
        {
            _birthInfo = new BirthInfo();
            return ApplyChanges();
        }
        public T WithBirthDay(string birthDate)
        {
            _birthInfo.BirthDate = birthDate;
            return ApplyChanges();
        }
        private T ApplyChanges()
        {
            AccountReport.AccountHolder.Individual.BirthInfo = _birthInfo;
            return (T)this;
        }
    }
    public class AccountBalanceBuilder<T> : BirthInfoBuilder<AccountBalanceBuilder<T>> where T : AccountBalanceBuilder<T>
    {
        private AccountBalance _accountBalance;
        public T AddAccountBalance()
        {
            _accountBalance = new AccountBalance();
            return ApplyChanges();
        }
        public T WithCurrencyCode(string currencyCode)
        {
            _accountBalance.CurrCode = currencyCode;
            return ApplyChanges();
        }

        public T WithAmount(string amount)
        {
            //Only slect numbers
            _accountBalance.Text = string.Concat(amount.Where(c => char.IsDigit(c) || c == '.').ToArray());
            return ApplyChanges();
        }
        public T Apply()
        {
            _accountReports.Insert(0, AccountReport);
            return ApplyChanges();
        }

        private T ApplyChanges()
        {
            AccountReport.AccountBalance = _accountBalance;
            return (T)this;
        }
    }
}
