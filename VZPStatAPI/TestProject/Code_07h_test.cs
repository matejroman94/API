using Common;
using Domain.DataDTO;
using Domain.Models;
using Moq;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZPStat.EventAsByte;
using VZPStatAPI.FakeData;

namespace TestProject
{
    public class Code_07h_test
    {
        private Faker faker;
        private List<EventPutDTO> eventPutDTOs;
        private List<Event> events;
        private List<EventAsByte> eventAsBytes;

        [SetUp]
        public void Setup()
        {
            faker = new Faker();
            eventPutDTOs = new List<EventPutDTO>();
            events = new List<Event>();
        }

        [Test]
        public void _codes_07h_Success()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_07h();
            bool res = true;

            foreach (var eventAs in eventAsBytes)
            {
                try
                {
                    EventPutDTO eventPutDTO;
                    EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                    eventPutDTO.BranchId = 3;
                    eventPutDTO.FullyProcessed = true;
                    eventPutDTOs.Add(eventPutDTO);
                }
                catch (Exception ex)
                {
                    res = false;
                    Console.WriteLine(ex);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException);
                    }
                }
            }

            int i = 0;

            // Act
            try
            {
                foreach (var eventAsByte in eventAsBytes)
                {
                    if (eventPutDTOs.Count <= i)
                    {
                        throw new Exception("EventPutDTOs length exceeded");
                    }

                    if (eventPutDTOs[i].EventCode is not null)
                    {
                        if (eventPutDTOs[i].EventNameId == 13)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.SignInClerkSucceed(ref @event);
                        }
                        else if (eventPutDTOs[i].EventNameId == 14)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.LogOffClerkSucceed(ref @event);
                        }
                        else if (eventPutDTOs[i].EventNameId == 15)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.SignInCounterSucceed(ref @event);
                        }
                        else if (eventPutDTOs[i].EventNameId == 16)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.LogOffCounterSucceed(ref @event);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _codes_07h_Success function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventHex);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _codes_07h_Success function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _codes_codes_07h function failed: eventPutDTO eventCode is null: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }
    }
}
