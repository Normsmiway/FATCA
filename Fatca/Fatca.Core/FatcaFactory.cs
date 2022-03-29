using Fatca.Core.Builders;
using Fatca.Core.Configurations;
using Fatca.Core.Extensions;
using Fatca.Core.Models;
using Fatca.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Fatca.Core
{
    public class FatcaFactory
    {
        private static IServiceProvider serviceProvider;
        private static readonly ReportSettings _setting;

        static FatcaFactory()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var services = new ServiceCollection();
            services.AddSingleton(Configuration);

            var reportSettings = Configuration.GetOptions<ReportSettings>("ReportSettings");
            services.AddSingleton(reportSettings);

            serviceProvider = services.BuildServiceProvider();

            _setting = serviceProvider.GetService<ReportSettings>();
        }

        /// <summary>
        /// Used to create single fatca report for multipe acccount holder,
        /// this is used in case single FI are uploaded in the same excel sheet
        /// </summary>
        /// <returns></returns>
        public static FATCA_OECD CreateFatca(List<FatcaReport> reports, DateTime reportingPeriod)
        {
            // $"{_fatca.ReportingFI.TIN}-PR".ToTimedId()
            if (reports.Any())
            {
                var msv = reports.FirstOrDefault();

                var AccountReports = new List<AccountReport>();
                reports.ForEach(accountReport =>
                {

                    AccountReports.Add(new AccountReport()
                    {
                        DocSpec = new DocSpec()
                        {
                            DocTypeIndic = "FATCA1",
                            //DocRefId = "ZGJY42.00000.LE.566.AR-9244547-20200521T132537"
                        },
                        AccountNumber = accountReport.AccountNumber,
                        AccountBalance = new AccountBalance()
                        {
                            CurrCode = accountReport.AccountCurrency,
                            Text = accountReport.AccountBalance
                        },
                        AccountHolder = new AccountHolder()
                        {
                            Individual = new Individual()
                            {
                                Address = new Address()
                                {
                                    AddressFix = new AddressFix()
                                    {
                                        BuildingIdentifier = accountReport.BuildingIdentifier ?? "N/A",
                                        City = accountReport.City,
                                        CountrySubentity = accountReport.CountrySubentity,
                                        DistrictName = accountReport.DistrictName,
                                        FloorIdentifier = accountReport.FloorIdentifier,
                                        POB = accountReport.POB,
                                        PostCode = accountReport.PostCode,
                                        Street = accountReport.StreetName,
                                        SuiteIdentifier = accountReport.SuiteIdentifier
                                    },
                                    CountryCode = accountReport.CountryCode,
                                },
                                BirthInfo = new BirthInfo()
                                {
                                    BirthDate = accountReport.DateOfBirth,
                                },
                                Name = new Name()
                                {
                                    FirstName = accountReport.FirstName,
                                    LastName = accountReport.LastName,
                                    MiddleName = accountReport.MiddleName
                                },
                                TIN = accountReport.TIN,
                            }
                        }
                    });
                });
                #region building report
                var report = new FATCA_OECD()
                {
                    #region attribute settings
                    Ftc = _setting.Global?.Ftc ?? "urn:oecd:ties:fatca:v2",
                    Version = _setting.Global?.Version ?? "2.0",
                    Iso = _setting.Global?.Iso ?? "urn:oecd:ties:isofatcatypes:v1",
                    SchemaLocation = _setting.Global?.SchemaLocation ?? "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd",
                    Sfa = _setting.Global?.Sfa ?? "urn:oecd:ties:stffatcatypes:v2",
                    Stf = _setting.Global?.Stf ?? "urn:oecd:ties:stf:v4",
                    //   Xsi = "http://www.w3.org/2001/XMLSchema-instance",
                    #endregion
                    MessageSpec = new MessageSpec()
                    {
                        TransmittingCountry = _setting.MessageSpec?.TransmittingCountry ?? "NG",
                        MessageType = _setting.MessageSpec?.MessageType ?? "FATCA",
                        ReceivingCountry = _setting.MessageSpec?.ReceivingCountry ?? "US",
                        ReportingPeriod = new DateTime(reportingPeriod.Year, reportingPeriod.Month, reportingPeriod.Day).ToString("yyyy-MM-dd"),
                        SendingCompanyIN = _setting.MessageSpec?.SendingCompanyIN ?? "N/A",
                        Timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                    },
                    FATCA = new FATCA()
                    {
                        ReportingFI = new ReportingFI()
                        {
                            ResCountryCode = msv?.ResCountryCode ?? "NG",
                            TIN = _setting.MessageSpec.TIN,// "ZGJY42.00000.LE.566",
                            Name = new Name()
                            {
                                Text = new string[] { msv?.FIName }
                            },
                            Address = new Address()
                            {
                                CountryCode = msv?.CountryCode,
                                AddressFix = new AddressFix()
                                {
                                    Street = _setting.FIAddress.Street ?? "DANMOLE STREET",
                                    BuildingIdentifier = _setting.FIAddress.BuildingIdentifier ?? "PLOT 999C",
                                    SuiteIdentifier = _setting.FIAddress.SuiteIdentifier ?? "N/A",
                                    FloorIdentifier = _setting.FIAddress.FloorIdentifier ?? "N/A",
                                    DistrictName = _setting.FIAddress.DistrictName ?? "VICTORIA ISLAND",
                                    POB = _setting.FIAddress.POB ?? "P.M.B 80150",
                                    PostCode = _setting.FIAddress.PostCode ?? "N/A",
                                    City = _setting.FIAddress.City ?? "LAGOS",
                                    CountrySubentity = _setting.FIAddress.CountrySubentity ?? "NIGERIA"
                                },
                            },
                            FilerCategory = "FATCA601",
                            DocSpec = new DocSpec()
                            {
                                DocTypeIndic = "FATCA1",
                                // DocRefId = "ZGJY42.00000.LE.566.FI20200521T132537004003"
                            }
                        },

                        ReportingGroup = new ReportingGroup()
                        {

                            AccountReports = AccountReports
                        }
                    },
                };
                //Generate a Unique number (Increment by one for every account holder)
                int id = new Random().Next(9297272, 9999999);
                report?.FATCA?.ReportingGroup?.AccountReports?.ForEach(c =>
                {
                    c.DocSpec.DocRefId = $"{_setting.MessageSpec.TIN}.AR-{id++}".ToTimedId();
                });

                //  report?.FATCA?.ReportingGroup?.AccountReports
                //Asign Report MessageRefId
                report.MessageSpec.MessageRefId = $"{report?.FATCA.ReportingFI.TIN.ToTimedId(report.MessageSpec.Timestamp)}";
                #endregion
                var messageSpec = CreateMessageSpec(report.MessageSpec, reportingPeriod);
                var fatca = CreateSingleFATCAReportForMultipleAccountHolder(report.FATCA);

                var builderReport = FatcaReportBuilder.Create()
                    .WithMessageSpecBlock(messageSpec)
                    .WithFatcaBlock(fatca)
                    //.AddPoolReport() //TODO: Build correct PoolReport
                    .ConfigureAttributes()
                    .AddVersion()
                    .Build();

                string path = Path.Combine(_setting.FilePath, $"{builderReport.MessageSpec.MessageRefId}.xml");
                try
                {
                    using (StreamWriter fatcaXml = new StreamWriter(path, false))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(FATCA_OECD));
                        serializer.Serialize(fatcaXml, builderReport);
                    }
                }
                catch (Exception ex)
                {
                }
                return builderReport;
            }
            throw new ArgumentNullException("Reports cannot be empty");
        }
        /// <summary>
        /// Used to create single fatca report for multipe acccount holder,
        /// this is used in case single FI are uploaded in the same excel sheet
        /// </summary>
        /// <returns></returns>
        public static FATCA_OECD CreateFatca()
        {
            #region test report
            var report = new FATCA_OECD()
            {
                #region attribute settings
                Ftc = "urn:oecd:ties:fatca:v2",
                Version = "2.0",
                Iso = "urn:oecd:ties:isofatcatypes:v1",
                SchemaLocation = "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd",
                Sfa = "urn:oecd:ties:stffatcatypes:v2",
                Stf = "urn:oecd:ties:stf:v4",
                //   Xsi = "http://www.w3.org/2001/XMLSchema-instance",
                #endregion
                MessageSpec = new MessageSpec()
                {
                    TransmittingCountry = "NG",
                    MessageRefId = "ZGJY42.00000.LE.566-20200521T132537",
                    MessageType = "FATCA",
                    ReceivingCountry = "US",
                    ReportingPeriod = new DateTime(2019, 12, 31).ToString("yyyy-MM-dd"),
                    SendingCompanyIN = "G5ME2G.00093.ME.999",
                    Timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                },
                FATCA = new FATCA()
                {
                    ReportingFI = new ReportingFI()
                    {
                        ResCountryCode = "NG",
                        TIN = "ZGJY42.00000.LE.566",
                        Name = new Name()
                        {
                            Text = new string[] { "Access Bank Plc" }
                        },
                        Address = new Address()
                        {
                            CountryCode = "NG",
                            AddressFix = new AddressFix()
                            {
                                Street = "DANMOLE STREET",
                                BuildingIdentifier = "PLOT 999C",
                                SuiteIdentifier = "N/A",
                                FloorIdentifier = "N/A",
                                DistrictName = "VICTORIA ISLAND",
                                POB = "P.M.B 80150",
                                PostCode = "N/A",
                                City = "LAGOS",
                                CountrySubentity = "NIGERIA"
                            },
                        },
                        FilerCategory = "FATCA601",
                        DocSpec = new DocSpec()
                        {
                            DocTypeIndic = "FATCA1",
                            DocRefId = "ZGJY42.00000.LE.566.FI20200521T132537004003"
                        }
                    },
                    ReportingGroup = new ReportingGroup()
                    {
                        AccountReports = new List<AccountReport>
                  {
                           new AccountReport()
                           {
                               DocSpec=new DocSpec()
                               {
                                   DocTypeIndic="FATCA1",
                                   DocRefId="ZGJY42.00000.LE.566.AR-9244547-20200521T132537"
                               },
                               AccountNumber="803852376",
                               AccountHolder=new AccountHolder()
                               {
                                   Individual=new Individual()
                                   {
                                      TIN="421-89-4528",
                                      Name=new Name()
                                      {
                                          FirstName="WILSON",
                                          MiddleName="ONYEDIKACHI",
                                          LastName="AGOMUO"
                                      },
                                      Address=new Address()
                                      {
                                          CountryCode="US",
                                          AddressFix=new AddressFix()
                                          {
                                              Street="WELLS BRANCH PKWY",
                                              BuildingIdentifier="7600",
                                              SuiteIdentifier="3300",
                                              PostCode="78728",
                                              City="TEXAS"
                                          }
                                      },
                                      BirthInfo=new BirthInfo()
                                      {
                                          BirthDate=new DateTime(1980,06,10).ToString("yyyy-MM-dd"),
                                      }
                                   }
                               },
                               AccountBalance=new AccountBalance()
                               {
                                   CurrCode="NGN",
                                   Text="24459488.68",
                               },
                           },
                            new AccountReport()
                           {
                               DocSpec=new DocSpec()
                               {
                                   DocTypeIndic="FATCA2",
                                   DocRefId="ZGJY42.00000.LE.566.AR-9244547-20200521T132537"
                               },
                               AccountNumber="903852376",
                               AccountHolder=new AccountHolder()
                               {
                                   Individual=new Individual()
                                   {
                                      TIN="421-89-4528",
                                      Name=new Name()
                                      {
                                          FirstName="WILSON2",
                                          MiddleName="ONYEDIKACHI2",
                                          LastName="AGOMUO2"
                                      },
                                      Address=new Address()
                                      {
                                          CountryCode="AU",
                                          AddressFix=new AddressFix()
                                          {
                                              Street="WELLS BRANCH PKWY 2",
                                              BuildingIdentifier="7600",
                                              SuiteIdentifier="3300",
                                              PostCode="78728",
                                              City="TEXAS"
                                          }
                                      },
                                      BirthInfo=new BirthInfo()
                                      {
                                          BirthDate=new DateTime(1980,06,10).ToString("yyyy-MM-dd"),
                                      }
                                   }
                               },
                               AccountBalance=new AccountBalance()
                               {
                                   CurrCode="NGN",
                                   Text="10000000.68",
                               },
                           }
                  }
                    }
                },
            };
            #endregion
            var messageSpec = CreateMessageSpec(report.MessageSpec, DateTime.Now);
            var fatca = CreateSingleFATCAReportForMultipleAccountHolder(report.FATCA);

            var builderReport = FatcaReportBuilder.Create()
                .WithMessageSpecBlock(messageSpec)
                .WithFatcaBlock(fatca)
             //   .AddPoolReport() //TODO: Build correct PoolReport
                .ConfigureAttributes()
                .AddVersion()
                .Build();
            string path = Path.Combine(@"C:\FATCA", $"{nameof(FatcaFactory)}{DateTime.Now.Ticks}.xml");
            try
            {
                using (StreamWriter fatcaXml = new StreamWriter(path, false))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(FATCA_OECD));
                    mySerializer.Serialize(fatcaXml, builderReport);
                }
            }
            catch (Exception ex)
            {
            }
            return builderReport;
        }

        /// <summary>
        /// Used to create separate fatca report for each acccount holder,
        /// this is used in case multiple FI are uploaded in the same excel sheet
        /// </summary>
        /// <returns></returns>
        public static FATCA_OECD CreateAndForwardSingle()
        {
            return new FATCA_OECD();
        }

        private static FATCA CreateSingleFATCAReportForMultipleAccountHolder(FATCA fatca)
        {
            FATCA result = null;

            foreach (var item in fatca.ReportingGroup.AccountReports)
            {
                FATCA ff = CreateReport(fatca, item);
                if (result is null)
                    result = ff;
                else
                    result.ReportingGroup.AccountReports.Add(ff.ReportingGroup.AccountReports.FirstOrDefault());
            }

            return result;
        }
        private static List<FATCA_OECD> CreateMultipleFATCAReportFromSingleFile()
        {
            List<FATCA_OECD> results = new List<FATCA_OECD>();
            #region test report
            var report = new FATCA_OECD()
            {
                #region attribute settings
                Ftc = "urn:oecd:ties:fatca:v2",
                Version = "2.0",
                Iso = "urn:oecd:ties:isofatcatypes:v1",
                SchemaLocation = "urn:oecd:ties:fatca:v2 FatcaXML_v2.0.xsd",
                Sfa = "urn:oecd:ties:stffatcatypes:v2",
                Stf = "urn:oecd:ties:stf:v4",
                //   Xsi = "http://www.w3.org/2001/XMLSchema-instance",
                #endregion
                MessageSpec = new MessageSpec()
                {
                    TransmittingCountry = "NG",
                    MessageRefId = "ZGJY42.00000.LE.566-20200521T132537",
                    MessageType = "FATCA",
                    ReceivingCountry = "US",
                    ReportingPeriod = new DateTime(2019, 12, 31).ToString("yyyy-MM-dd"),
                    SendingCompanyIN = "G5ME2G.00093.ME.999",
                    Timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                },
                FATCA = new FATCA()
                {
                    ReportingFI = new ReportingFI()
                    {
                        ResCountryCode = "NG",
                        TIN = "ZGJY42.00000.LE.566",
                        Name = new Name()
                        {
                            Text = new string[] { "Access Bank Plc" }
                        },
                        Address = new Address()
                        {
                            CountryCode = "NG",
                            AddressFix = new AddressFix()
                            {
                                Street = "DANMOLE STREET",
                                BuildingIdentifier = "PLOT 999C",
                                SuiteIdentifier = "N/A",
                                FloorIdentifier = "N/A",
                                DistrictName = "VICTORIA ISLAND",
                                POB = "P.M.B 80150",
                                PostCode = "N/A",
                                City = "LAGOS",
                                CountrySubentity = "NIGERIA"
                            },
                        },
                        FilerCategory = "FATCA601",
                        DocSpec = new DocSpec()
                        {
                            DocTypeIndic = "FATCA1",
                            DocRefId = "ZGJY42.00000.LE.566.FI20200521T132537004003"
                        }
                    },
                    ReportingGroup = new ReportingGroup()
                    {
                        AccountReports = new List<AccountReport>
                  {
                           new AccountReport()
                           {
                               DocSpec=new DocSpec()
                               {
                                   DocTypeIndic="FATCA1",
                                   DocRefId="ZGJY42.00000.LE.566.AR-9244547-20200521T132537"
                               },
                               AccountNumber="803852376",
                               AccountHolder=new AccountHolder()
                               {
                                   Individual=new Individual()
                                   {
                                      TIN="421-89-4528",
                                      Name=new Name()
                                      {
                                          FirstName="WILSON",
                                          MiddleName="ONYEDIKACHI",
                                          LastName="AGOMUO"
                                      },
                                      Address=new Address()
                                      {
                                          CountryCode="US",
                                          AddressFix=new AddressFix()
                                          {
                                              Street="WELLS BRANCH PKWY",
                                              BuildingIdentifier="7600",
                                              SuiteIdentifier="3300",
                                              PostCode="78728",
                                              City="TEXAS"
                                          }
                                      },
                                      BirthInfo=new BirthInfo()
                                      {
                                          BirthDate=new DateTime(1980,06,10).ToString("yyyy-MM-dd"),
                                      }
                                   }
                               },
                               AccountBalance=new AccountBalance()
                               {
                                   CurrCode="NGN",
                                   Text="24459488.68",
                               },
                           },
                            new AccountReport()
                           {
                               DocSpec=new DocSpec()
                               {
                                   DocTypeIndic="FATCA2",
                                   DocRefId="ZGJY42.00000.LE.566.AR-9244547-20200521T132537"
                               },
                               AccountNumber="903852376",
                               AccountHolder=new AccountHolder()
                               {
                                   Individual=new Individual()
                                   {
                                      TIN="421-89-4528",
                                      Name=new Name()
                                      {
                                          FirstName="WILSON2",
                                          MiddleName="ONYEDIKACHI2",
                                          LastName="AGOMUO2"
                                      },
                                      Address=new Address()
                                      {
                                          CountryCode="AU",
                                          AddressFix=new AddressFix()
                                          {
                                              Street="WELLS BRANCH PKWY 2",
                                              BuildingIdentifier="7600",
                                              SuiteIdentifier="3300",
                                              PostCode="78728",
                                              City="TEXAS"
                                          }
                                      },
                                      BirthInfo=new BirthInfo()
                                      {
                                          BirthDate=new DateTime(1980,06,10).ToString("yyyy-MM-dd"),
                                      }
                                   }
                               },
                               AccountBalance=new AccountBalance()
                               {
                                   CurrCode="NGN",
                                   Text="10000000.68",
                               },
                           }
                  }
                    }
                },
            };
            #endregion
            var messageSpec = CreateMessageSpec(report.MessageSpec, DateTime.Now);
            var fatca = CreateSingleFATCAReportForMultipleAccountHolder(report.FATCA);

            var builderReport = FatcaReportBuilder.Create()
                .WithMessageSpecBlock(messageSpec)
                .WithFatcaBlock(fatca)
               // .AddPoolReport()
                .ConfigureAttributes()
                .AddVersion()
                .Build();
            string path = Path.Combine(@"C:\FATCA", $"{nameof(FatcaFactory)}{DateTime.Now.Ticks}.xml");
            try
            {
                using (StreamWriter fatcaXml = new StreamWriter(path, false))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(FATCA_OECD));
                    mySerializer.Serialize(fatcaXml, builderReport);
                }
            }
            catch (Exception ex)
            {
            }

            return results;
        }
        private static FATCA CreateReport(FATCA report, AccountReport item)
        {
            return Create.ReportingFinanciaIInstitution
                          .WithResCountryCode(report.ReportingFI.ResCountryCode)
                          .AndTaxIndentificationNumber(report.ReportingFI.TIN).WithFilerCategory(report.ReportingFI.FilerCategory)
                          //Add Name Block
                          .IncludeName().WithFirstNameAs(report.ReportingFI.Name.FirstName).AndMiddleName(report.ReportingFI.Name.MiddleName)
                                    .ThenLastName(report.ReportingFI.Name.LastName).WithParams(report.ReportingFI.Name.Text)
                          //Add Address Block
                          .AddAddress().WithCountryCode(report.ReportingFI.Address.CountryCode)
                                        //Add AddressFix Block
                                        .IncludingAddressFix().At(report.ReportingFI.Address.AddressFix.Street)
                                                              .WithBuildingIdentifier(report.ReportingFI.Address.AddressFix.BuildingIdentifier)
                                                              .AndSuiteIdentifier(report.ReportingFI.Address.AddressFix.SuiteIdentifier)
                                                              .WithFloorIdentifier(report.ReportingFI.Address.AddressFix.FloorIdentifier)
                                                              .WithDistrictName(report.ReportingFI.Address.AddressFix.DistrictName)
                                                              .AndPOB(report.ReportingFI.Address.AddressFix.POB)
                                                              .WithPostCode(report.ReportingFI.Address.AddressFix.PostCode)
                                                              .In(report.ReportingFI.Address.AddressFix.City)
                                                              .WithCountrySubentity(report.ReportingFI.Address.AddressFix.CountrySubentity)
                          //Add DocSpec Block
                          .AlsoAddDocSpec().OfDocTypeIndic(report.ReportingFI.DocSpec.DocTypeIndic).WithDocRefId(report.ReportingFI.DocSpec.DocRefId)

                  //Reporting Group 
                  .ReportingGroup
                         .AddAccountReports().WithAccountNumber(item.AccountNumber)
                                             .AddDocSpec().WithDocTypeIndic(item.DocSpec.DocTypeIndic).WithDocRefId(item.DocSpec.DocRefId)
                                             .AddACoountHolder()
                                                             //Add Individual or Organization Entity
                                                             .AddIndividual().WithIndividualTIN(item.AccountHolder.Individual.TIN)
                                                                             .AddName().WithFirstName(item.AccountHolder.Individual.Name.FirstName)
                                                                                       .WithMiddleName(item.AccountHolder.Individual.Name.MiddleName)
                                                                                       .WithLastName(item.AccountHolder.Individual.Name.LastName)
                                                                                       .WithParams(item.AccountHolder.Individual.Name.Text)
                                                                             .AddAddress().WithCountryCode(item.AccountHolder.Individual.Address.CountryCode)
                                                                                           //Add AddressFix Block
                                                                                           .AddAddressFix().WithStreet(item.AccountHolder.Individual.Address.AddressFix.Street)
                                                                                                           .WithBuildingIdentifier(item.AccountHolder.Individual.Address.AddressFix.BuildingIdentifier)
                                                                                                           .WithSuiteIdentifier(item.AccountHolder.Individual.Address.AddressFix.SuiteIdentifier)
                                                                                                           .WithFloorIdentifier(item.AccountHolder.Individual.Address.AddressFix.FloorIdentifier)
                                                                                                           .WithDistrictName(item.AccountHolder.Individual.Address.AddressFix.DistrictName)
                                                                                                           .WithPOB(item.AccountHolder.Individual.Address.AddressFix.POB)
                                                                                                           .WithPostCode(item.AccountHolder.Individual.Address.AddressFix.PostCode)
                                                                                                           .WithCity(item.AccountHolder.Individual.Address.AddressFix.City)
                                                                                                           .WithCountrySubentity(item.AccountHolder.Individual.Address.AddressFix.CountrySubentity)
                                                                                           .AddBirthInfo().WithBirthDay(item.AccountHolder.Individual.BirthInfo.BirthDate)
                                             .AddAccountBalance().WithCurrencyCode(item.AccountBalance.CurrCode).WithAmount(item.AccountBalance.Text).Apply()
                  .Build();

        }

        private static MessageSpec CreateMessageSpec(MessageSpec MessageSpec, DateTime period)
        {
            return MessageSpecBuilder.Create()
              .Info
                .From(MessageSpec.SendingCompanyIN)
                .WithMesageRefId(MessageSpec.MessageRefId)
                .WithMessageType(MessageSpec.MessageType)
                .WithPeriod(period)
                .At(DateTime.Now)
              .Transmitting
                .From(MessageSpec.TransmittingCountry)
                .To(MessageSpec.ReceivingCountry)
              .Build();
        }
    }
}