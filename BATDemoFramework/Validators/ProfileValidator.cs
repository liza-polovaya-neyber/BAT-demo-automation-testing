using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoFramework.Generators;
using BATDemoFramework.Models;
using BATDemoSalesForce.Repos;
using NUnit.Framework;

namespace BATDemoTests.Validators
{
    public class ProfileValidator : IProfileValidator
    {
        private readonly IProfileRepository profileRepository;

        public ProfileValidator(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public async Task AssertCreatedProfileState(UserLoginModel user)
        {
            var profile = await profileRepository.GetByEmailAsync(user.Email);
            Assert.AreEqual(user.Title, profile.Title);
            Assert.AreEqual(user.FirstName, profile.FirstName);
            Assert.AreEqual(user.Surname, profile.LastName);
            Assert.AreEqual(user.Email, profile.Email);
            Assert.AreEqual(user.DoB, profile.Birthdate);
            Assert.AreEqual(user.MobilePhone, profile.MobilePhone);
            Assert.AreEqual(user.TenantName, profile.ClientId);
        }

        public async Task AssertCreatedProfileStateSSO(SSOUser user, User newUser)
        {
            var profile = await profileRepository.GetByEmailAsync(user.Email);
            var birthDate = profile.Birthdate?.ToString("MM/dd/yyyy");
            var employmentStartDate = profile.ConfirmedEmploymentStartDate?.ToString("d");
            Assert.AreEqual("Mr", profile.Title);
            Assert.AreEqual(user.FirstName, profile.FirstName);
            Assert.AreEqual(user.Surname, profile.LastName);
            Assert.AreEqual(user.Email, profile.Email);
            Assert.AreEqual(newUser.EmailAddress, profile.SecondaryEmail);
            Assert.AreEqual(user.DOB, birthDate);
            Assert.AreEqual("07523698547", profile.MobilePhone);
            Assert.AreEqual(user.Salary, profile.ConfirmedGrossAnnualSalary.ToString());
            Assert.AreEqual(user.EmployeeId, profile.EmployeeId);
            Assert.AreEqual(user.JobTitle, profile.JobTitle);
            Assert.AreEqual(user.JoiningDate, employmentStartDate);
        }
    }
}
