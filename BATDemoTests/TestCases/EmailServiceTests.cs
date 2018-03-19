﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BATDemoFramework.EmailService;
using Google;
using NUnit.Framework;

namespace BATDemoTests.TestCases
{
    [TestFixture]
    public class EmailServiceTests
    {
        private EmailService service;

        [SetUp]
        public void Setup()
        {
            service = new EmailService();
        }

        [Test]
        public async Task RetreiveMessage_ByValidSubject_SubjectIsEqual()
        {
            //Arrange
            
            //Act
            var messages = await service.GetMessagesByQuery(EmailTypes.ConfirmYourEmail);
            var subject = messages[0].Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value;

            //Assert
            Assert.AreEqual(EmailTypes.ConfirmYourEmail, subject);
        }

        [Test]
        public async Task RetreiveMessage_InvalidQuery_NoMessageProvided()
        {
            //Arrange
            
            //Act
            var messages = await service.GetMessagesByQuery("query that gives no messages back");

            //Assert
            Assert.IsEmpty(messages);
        }

        [Test]
        public async Task GetToken_ValidEmail_Success()
        {
            //Arrange
            var guidLength = 32;
            var messages = await service.GetMessagesByQuery(EmailTypes.ConfirmYourEmail);

            //Act
            var token = service.GetTokenFromEmail(messages[0]);

            //Assert
            Assert.AreEqual(guidLength, token.Length);
        }

        [Test]
        public async Task GetToken_InvalidEmail_ThowsException()
        {
            //Arrange
            var messages = await service.GetMessagesByQuery("Stay more organized");

            //Act
            

            //Assert
            Assert.Throws<Exception>(() => service.GetTokenFromEmail(messages[0]),
                $"Message doesn't have required subject: {EmailTypes.ConfirmYourEmail}");
        }

        [Test]
        public async Task DeleteMessage_ValidId_NoMessageInTheInbox()
        {
            //Arrange
            var idToDelete = "1623e30e280d83cb";
            var messages = await service.GetMessagesByQuery("hello");

            //Act
            service.DeleteMessage(idToDelete);
            messages = await service.GetMessagesByQuery("hello");


            //Assert
            Assert.IsEmpty(messages.Where(x=>x.Id == idToDelete));
        }

        [Test]
        public void DeleteMessage_InvalidId_NoMessageInTheInbox()
        {
            //Arrange
            var idToDelete = "564";

            //Act


            //Assert
            Assert.Throws<GoogleApiException>(() => service.DeleteMessage(idToDelete));
        }
    }
}