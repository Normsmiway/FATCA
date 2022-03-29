using Fatca.Core.Extensions;
using Fatca.Core.Models;
using System;
using System.Linq;

namespace Fatca.Core
{
    public class FatcaReportBuilder : IMessageSpec, IFatca, IBlockAttribute, IPoolReport
    {
        #region fields
        private MessageSpec _messageSpec;
        private FATCA _fatca;
        private string _version;
        private string _ftc;
        private string _sfa;
        private string _stf;
        private string _schemaLocation;
        private string _iso;
        #endregion
        private FatcaReportBuilder()
        {

        }

        public static IMessageSpec Create() => new FatcaReportBuilder();

        public IFatca WithMessageSpecBlock(MessageSpec messageSpec)
        {
            _messageSpec = messageSpec;
            return this;
        }
        public IBlockAttribute WithFatcaBlock(FATCA fatca)
        {
            _fatca = fatca;
            return this;
        }
        public IBlockAttribute AddPoolReport()
        {
            var timestring = $"{string.Concat(DateTime.Today.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss").Where(c => Char.IsDigit(c)).ToArray())}";
            var poolBalance = new PoolBalance()
            {
                CurrCode = _fatca.ReportingGroup?.AccountReports?.FirstOrDefault()?.AccountBalance?.CurrCode ?? "NGN",
                Text = _fatca.ReportingGroup
                .AccountReports.Sum(c =>
                 Convert.ToDecimal(c.AccountBalance.Text))
                .ToString()
            };
            var poolReport = new PoolReport()
            {
                AccountCount = _fatca.ReportingGroup.AccountReports.Count().ToString(),
                AccountPoolReportType = "FATCA201",
                DocSpec = new DocSpec()
                {
                    DocRefId = $"{_fatca.ReportingFI.TIN}-PR".ToTimedId(),
                    DocTypeIndic = "FATCA1"
                },
                PoolBalance = poolBalance
            };
            _fatca.ReportingGroup.PoolReport = poolReport;
            return this;
        }
        public IBlockAttribute ConfigureAttributes(string iso = "urn:oecd:ties:isofatcatypes:v1", string ftc = "urn:oecd:ties:fatca:v2",
            string schemaLocation = "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd", string sfa = "urn:oecd:ties:stffatcatypes:v2", string stf = "urn:oecd:ties:stf:v4")
        {
            _iso = iso;
            _ftc = ftc;
            _schemaLocation = schemaLocation;
            _sfa = sfa;
            _stf = stf;
            return this;
        }
        public IBlockAttribute AddVersion(string version = "2.0")
        {
            _version = version;
            return this;
        }

        public FATCA_OECD Build()
        {
            return new FATCA_OECD()
            {
                MessageSpec = _messageSpec,
                FATCA = _fatca,
                Version = _version,
                Ftc = _ftc,
                Iso = _iso,
                SchemaLocation = _schemaLocation,
                Sfa = _sfa,
                Stf = _stf
            };
        }
    }


    public interface IMessageSpec
    {
        IFatca WithMessageSpecBlock(MessageSpec messageSpec);
    }
    public interface IFatca
    {
        IBlockAttribute WithFatcaBlock(FATCA fatca);
    }

    public interface IPoolReport
    {
        IBlockAttribute AddPoolReport();
    }
    public interface IBlockAttribute
    {
        IBlockAttribute ConfigureAttributes(string iso = "urn:oecd:ties:isofatcatypes:v1",
            string ftc = "urn:oecd:ties:fatca:v2", string schemaLocation = "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd",
            string sfa = "urn:oecd:ties:stffatcatypes:v2", string stf = "urn:oecd:ties:stf:v4");
        IBlockAttribute AddVersion(string version = "2.0");
        FATCA_OECD Build();
    }

}
