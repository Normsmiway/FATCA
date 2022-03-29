namespace Fatca.Test.XmlModels
{
    using System.Collections.Generic;
    /* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "MessageSpec", Namespace = "urn:oecd:ties:fatca:v2")]
    public class MessageSpec
    {
        [XmlElement(ElementName = "SendingCompanyIN", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string SendingCompanyIN { get; set; }
        [XmlElement(ElementName = "TransmittingCountry", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string TransmittingCountry { get; set; }
        [XmlElement(ElementName = "ReceivingCountry", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string ReceivingCountry { get; set; }
        [XmlElement(ElementName = "MessageType", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string MessageType { get; set; }
        [XmlElement(ElementName = "MessageRefId", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string MessageRefId { get; set; }
        [XmlElement(ElementName = "ReportingPeriod", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string ReportingPeriod { get; set; }
        [XmlElement(ElementName = "Timestamp", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string Timestamp { get; set; }
    }

    [XmlRoot(ElementName = "AddressFix", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class AddressFix
    {
        [XmlElement(ElementName = "Street", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string Street { get; set; }
        [XmlElement(ElementName = "BuildingIdentifier", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string BuildingIdentifier { get; set; }
        [XmlElement(ElementName = "SuiteIdentifier", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string SuiteIdentifier { get; set; }
        [XmlElement(ElementName = "FloorIdentifier", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string FloorIdentifier { get; set; }
        [XmlElement(ElementName = "DistrictName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string DistrictName { get; set; }
        [XmlElement(ElementName = "POB", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string POB { get; set; }
        [XmlElement(ElementName = "PostCode", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string PostCode { get; set; }
        [XmlElement(ElementName = "City", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string City { get; set; }
        [XmlElement(ElementName = "CountrySubentity", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string CountrySubentity { get; set; }
    }

    [XmlRoot(ElementName = "Address", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class Address
    {
        [XmlElement(ElementName = "CountryCode", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string CountryCode { get; set; }
        [XmlElement(ElementName = "AddressFix", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public AddressFix AddressFix { get; set; }
    }

    [XmlRoot(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
    public class DocSpec
    {
        [XmlElement(ElementName = "DocTypeIndic", Namespace = "urn:oecd:ties:fatca:v2")]
        public string DocTypeIndic { get; set; }
        [XmlElement(ElementName = "DocRefId", Namespace = "urn:oecd:ties:fatca:v2")]
        public string DocRefId { get; set; }
    }

    [XmlRoot(ElementName = "ReportingFI", Namespace = "urn:oecd:ties:fatca:v2")]
    public class ReportingFI
    {
        [XmlElement(ElementName = "ResCountryCode", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string ResCountryCode { get; set; }
        [XmlElement(ElementName = "TIN", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string TIN { get; set; }
        [XmlElement(ElementName = "Name", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public Name Name { get; set; }
        [XmlElement(ElementName = "Address", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "FilerCategory", Namespace = "urn:oecd:ties:fatca:v2")]
        public string FilerCategory { get; set; }
        [XmlElement(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public DocSpec DocSpec { get; set; }
    }

    [XmlRoot(ElementName = "Name", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class Name
    {
        private string[] textField;
        [XmlElement(ElementName = "FirstName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "MiddleName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string MiddleName { get; set; }
        [XmlElement(ElementName = "LastName", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string LastName { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    [XmlRoot(ElementName = "BirthInfo", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
    public class BirthInfo
    {
        [XmlElement(ElementName = "BirthDate", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string BirthDate { get; set; }
    }

    [XmlRoot(ElementName = "Individual", Namespace = "urn:oecd:ties:fatca:v2")]
    public class Individual
    {
        [XmlElement(ElementName = "TIN", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public string TIN { get; set; }
        [XmlElement(ElementName = "Name", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public Name Name { get; set; }
        [XmlElement(ElementName = "Address", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "BirthInfo", Namespace = "urn:oecd:ties:stffatcatypes:v2")]
        public BirthInfo BirthInfo { get; set; }
    }

    [XmlRoot(ElementName = "AccountHolder", Namespace = "urn:oecd:ties:fatca:v2")]
    public class AccountHolder
    {
        [XmlElement(ElementName = "Individual", Namespace = "urn:oecd:ties:fatca:v2")]
        public Individual Individual { get; set; }
    }

    [XmlRoot(ElementName = "AccountBalance", Namespace = "urn:oecd:ties:fatca:v2")]
    public class AccountBalance
    {
        [XmlAttribute(AttributeName = "currCode")]
        public string CurrCode { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "AccountReport", Namespace = "urn:oecd:ties:fatca:v2")]
    public class AccountReport
    {
        [XmlElement(ElementName = "DocSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public DocSpec DocSpec { get; set; }
        [XmlElement(ElementName = "AccountNumber", Namespace = "urn:oecd:ties:fatca:v2")]
        public string AccountNumber { get; set; }
        [XmlElement(ElementName = "AccountHolder", Namespace = "urn:oecd:ties:fatca:v2")]
        public AccountHolder AccountHolder { get; set; }
        [XmlElement(ElementName = "AccountBalance", Namespace = "urn:oecd:ties:fatca:v2")]
        public AccountBalance AccountBalance { get; set; }
    }

    [XmlRoot(ElementName = "ReportingGroup", Namespace = "urn:oecd:ties:fatca:v2")]
    public class ReportingGroup
    {
        [XmlElement(ElementName = "AccountReport", Namespace = "urn:oecd:ties:fatca:v2")]
        public AccountReport[] AccountReport { get; set; }
    }

    [XmlRoot(ElementName = "FATCA", Namespace = "urn:oecd:ties:fatca:v2")]
    public class FATCA
    {
        [XmlElement(ElementName = "ReportingFI", Namespace = "urn:oecd:ties:fatca:v2")]
        public ReportingFI ReportingFI { get; set; }
        [XmlElement(ElementName = "ReportingGroup", Namespace = "urn:oecd:ties:fatca:v2")]
        public ReportingGroup ReportingGroup { get; set; }
    }

    [XmlRoot(ElementName = "FATCA_OECD", Namespace = "urn:oecd:ties:fatca:v2")]
    public class FATCA_OECD
    {
        [XmlElement(ElementName = "MessageSpec", Namespace = "urn:oecd:ties:fatca:v2")]
        public MessageSpec MessageSpec { get; set; }
        [XmlElement(ElementName = "FATCA", Namespace = "urn:oecd:ties:fatca:v2")]
        public FATCA FATCA { get; set; }
        [XmlAttribute(AttributeName = "iso", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Iso { get; set; }
        [XmlAttribute(AttributeName = "ftc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ftc { get; set; }
        [XmlAttribute(AttributeName = "stf", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Stf { get; set; }
        [XmlAttribute(AttributeName = "sfa", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sfa { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

}


