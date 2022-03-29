using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fatca.Core.Utils
{
    public class FatcaReportManager
    {
        static readonly List<string> supportedFormats = new List<string>() { ".csv", ".xlsx", ".xls" };
        public static List<FatcaReport> ReadFromExcel(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (supportedFormats.Contains(Path.GetExtension(path).ToLowerInvariant()))
                    try
                    {
                        return ExcelManager.ReadFromExcel<List<FatcaReport>>(path) ?? Enumerable.Empty<FatcaReport>().ToList();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                throw new FormatException("Unsupported File format");
            }

            throw new ArgumentNullException($"{nameof(path)} cannot be empty");
        }

        public static List<FatcaReport> ReadFromExcel(Stream stream)
        {
            try
            {
                return ExcelManager.ReadFromExcel<List<FatcaReport>>(stream) ?? Enumerable.Empty<FatcaReport>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class FatcaReport
    {
        public string ReportingYear { get; set; } = DateTime.Today.Year.ToString();
        public string AccountNumber { get; set; } = "N/A";
        public string AccountClosed { get; set; } = "N/A";
        public string CategoryOfAccountHolder { get; set; } = "Individual";
        public string AccountHolderType { get; set; } = "N/A";
        public string FIName { get; set; } = "N/A";
        public string FirstName { get; set; } = "N/A";
        public string MiddleName { get; set; } = "N/A";
        public string LastName { get; set; } = "N/A";
        public string DateOfBirth { get; set; } = "N/A";
        public string EntityName { get; set; } = "N/A";
        public string TIN { get; set; } = "N/A";
        public string ResCountryCode { get; set; } = "N/A";
        public string CountryCode { get; set; } = "N/A";
        public string StreetName { get; set; } = "N/A";
        public string BuildingIdentifier { get; set; } = "N/A";
        public string SuiteIdentifier { get; set; } = "N/A";
        public string FloorIdentifier { get; set; } = "N/A";
        public string DistrictName { get; set; } = "N/A";
        public string POB { get; set; } = "N/A";
        public string PostCode { get; set; } = "N/A";
        public string City { get; set; } = "N/A";
        public string CountrySubentity { get; set; } = "N/A";
        public string AddressFree { get; set; } = "N/A";
        public string AccountBalance { get; set; } = "N/A";
        public string AccountCurrency { get; set; } = "N/A";
        public string DividendsAmount { get; set; } = "N/A";
        public string DividendsCurrency { get; set; } = "N/A";
        public string InterestAmount { get; set; } = "N/A";
        public string InterestCurrency { get; set; } = "N/A";
        public string GrossProceedsRedemptionsAmount { get; set; } = "N/A";
        public string GrossProceedsRedemptionsCurrency { get; set; } = "N/A";
        public string OtherAmount { get; set; } = "N/A";
        public string OtherCurrency { get; set; } = "N/A";


        public override string ToString()
        {
            return $"This is {LastName} {MiddleName} {FirstName} FATCA report for the year {ReportingYear} as reported by {FIName} ({ResCountryCode})";
        }
    }
}
