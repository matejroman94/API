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
    public class Codes_x568h_test
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
        public void _code_x5hSuccees()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_05h();
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
                        if (eventPutDTOs[i].EventNameId == 10)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.TransferClientSucceed(ref @event);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _code_x5hSuccees function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventNameId);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _code_x5hSuccees function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _code_x5hSuccees function failed: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }

        [Test]
        public void _code_x6hSuccees()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_06h();
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
                        if (eventPutDTOs[i].EventNameId == 11)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.TransferClientSucceed(ref @event);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _code_x6hSuccees function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventHex);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _code_x6hSuccees function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _code_x6hSuccees function failed: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }

        [Test]
        public void _code_x8hSuccees()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_08h();
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
                        if (eventPutDTOs[i].EventNameId == 12)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.TransferClientSucceed(ref @event);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _code_x8hSuccees function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventHex);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _code_x8hSuccees function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _code_x8hSuccees function failed: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }
    }
}
