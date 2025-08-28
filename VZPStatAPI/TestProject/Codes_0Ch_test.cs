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
    public class Codes_0Ch_test
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
        public void _codes_0Ch_Success()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Ch();
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
                        if (eventPutDTOs[i].EventNameId == 22)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,1);
                        }
                        else if (eventPutDTOs[i].EventNameId == 23)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,2);
                        }
                        else if (eventPutDTOs[i].EventNameId == 24)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,3);
                        }
                        else if (eventPutDTOs[i].EventNameId == 25)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,4);
                        }
                        else if (eventPutDTOs[i].EventNameId == 26)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,5);
                        }
                        else if (eventPutDTOs[i].EventNameId == 27)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,6);
                        }
                        else if (eventPutDTOs[i].EventNameId == 28)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,7);
                        }
                        else if (eventPutDTOs[i].EventNameId == 29)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,8);
                        }
                        else if (eventPutDTOs[i].EventNameId == 30)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,9);
                        }
                        else if (eventPutDTOs[i].EventNameId == 31)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,10);
                        }
                        else if (eventPutDTOs[i].EventNameId == 32)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.AddDiagnosticEventToBranchSucceed(ref @event,11);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _codes_0Ch_Success function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventHex);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _codes_0Ch_Success function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _codes_0Ch_Success function failed: eventPutDTO eventCode is null: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }
    }
}
