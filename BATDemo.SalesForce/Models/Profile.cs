using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BATDemoSalesForce.Models
{
    public class Profile : SalesObject
    {
        [JsonIgnore]
        public override string ObjectTypeName
        {
            get { return "Contact"; }
        }

        public string RecordTypeId { get; set; }

        [JsonProperty("Tenant_Name__c")]
        public string TenantName { get; set; }

        [JsonProperty("Organisation_Name__c")]
        public string OrganisationName { get; set; }

        [JsonProperty("Finwell_Only__c")]
        public bool IsFinwellOnly { get; set; }

        [JsonProperty("Client_ID__c")]
        public string ClientId { get; set; }

        [JsonProperty("Employee_Reference__c")]
        public string EmployeeReference { get; set; }

        [JsonProperty("Salutation")]
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonProperty("Email_personal__c")]
        public string EmailPersonal { get; set; }

        [JsonProperty("Secondary_Email__c")]
        public string SecondaryEmail { get; set; }

        [JsonProperty("Secondary_Email_Status__c")]
        public string SecondaryEmailStatus { get; set; }

        public DateTime? Birthdate { get; set; }

        [JsonProperty("HomePhone")]
        public string HomePhone { get; set; }

        [JsonProperty("MobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("Security_Code__c")]
        public string PinCode { get; set; }

        [JsonProperty("Postcode__c")]
        public string PostalCode { get; set; }

        [JsonProperty("House_Number__c")]
        public string HouseNumber { get; set; }

        [JsonProperty("Flat_Number__c")]
        public string FlatNumber { get; set; }

        [JsonProperty("Housename__c")]
        public string HouseName { get; set; }

        [JsonProperty("Town_City__c")]
        public string City { get; set; }

        [JsonProperty("County__c")]
        public string County { get; set; }

        [JsonProperty("Address_line_1__c")]
        public string AddressLine1 { get; set; }

        [JsonProperty("Address_line_2__c")]
        public string AddressLine2 { get; set; }

        [JsonProperty("Year_Moved_In__c")]
        public string YearMovedIn { get; set; }

        [JsonProperty("Month_Moved_In__c")]
        public string MonthMovedIn { get; set; }

        [JsonProperty("Employment_Start_Date__c")]
        public DateTime? EmploymentStartDate { get; set; }

        [JsonProperty("Eligibility_Status__c")]
        public string EligibilityStatus { get; set; }

        [JsonProperty("Job_Title__c")]
        public string JobTitle { get; set; }

        [JsonProperty("National_Insurance_number__c")]
        public string NiNumber { get; set; }

        [JsonProperty("Police_force_Organisation__c")]
        public string PoliceForceOrganisation { get; set; }

        [JsonProperty("Employee_Id__c")]
        public string EmployeeId { get; set; }

        [JsonProperty("Terms_Privacy_Agreed__c")]
        public bool TermsAndPrivacyPolicyAccepted { get; set; }

        [JsonProperty("Agreed_to_Police_Mutual_Marketing__c")]
        public bool AgreedPoliceMutualMarketing { get; set; }

        [JsonProperty("Email_Status__c")]
        public string EmailStatus { get; set; }

        [JsonProperty("Marketing_e_mail__c")]
        public string MarketingEmail { get; set; }

        [JsonProperty("Marketing_post__c")]
        public string MarketingPost { get; set; }

        [JsonProperty("Marketing_SMS__c")]
        public string MarketingSms { get; set; }

        [JsonProperty("Marketing_Phone__c")]
        public string MarketingTelephone { get; set; }

        [JsonProperty("Is_Service_Contact__c")]
        public bool? IsServiceContact { get; set; }

        [JsonProperty("Data_Sharing_Agreed__c")]
        public bool DataSharingAgreed { get; set; }

        [JsonProperty("Payday__c")]
        public decimal? Payday { get; set; }

        [JsonProperty("Confirmed_Gross_Annual_Salary__c")]
        public decimal? ConfirmedGrossAnnualSalary { get; set; }

        [JsonProperty("Confirmed_Employment_Start_Date__c")]
        public DateTime? ConfirmedEmploymentStartDate { get; set; }
        [JsonProperty("Security_Profile_Id__c")]
        public string SecurityProfileId { get; set; }

        [JsonProperty("IMechE_Membership_Number__c")]
        public int? MembershipNumber { get; set; }

        [JsonProperty("Capita_GUID__c")]
        public string CapitaGuid { get; set; }

        [JsonProperty("PF_Consenttocont__c")]
        public bool EmployerConsent { get; set; }

        [JsonProperty("PF_Nationality__c")]
        public string Nationality { get; set; }

        [JsonProperty("Is_Eligible_For_Top_Up__c")]
        public bool IsEligibleForTopup { get; set; }

        [JsonProperty("ReferredBy__c")]
        public string ReferredBy { get; set; }

        [JsonProperty("FMR_Total_Gross_Income__c")]
        public string FMRTotalGrossIncome { get; set; }
    }
}
