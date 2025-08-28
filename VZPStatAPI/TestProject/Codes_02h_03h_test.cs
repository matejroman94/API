using AutoMapper;
using Common;
using Domain.DataDTO;
using Domain.MappingProfiles;
using Domain.Models;
using Moq;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VZPStat.EventAsByte;
using VZPStatAPI.FakeData;

namespace TestProject
{
    public class Codes_02h_03h_test
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
        public void _codes_02hSuccess()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_02h();
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
                        if (eventPutDTOs[i].EventNameId == 7)
                        {
                            var @event = eventPutDTOs[i];
                            res = CommonFunctions.InvokeClientSucceed(ref @event, false);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _codes_02hSuccess function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventHex);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _codes_02hSuccess function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _codes_02hSuccess function failed: eventPutDTO eventCode is null: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }

        [Test]
        public void _codes_03hSuccess()
        {
            // Arrange
            var mockContactRepository = new Mock<IUnitOfWork>();
            mockContactRepository.Setup(p => p.Events.AddRange(It.IsAny<IEnumerable<Event>>()))
              .Returns(true);

            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_03h();
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
                        if (eventPutDTOs[i].EventNameId == 8)
                        {
                            var @event = eventPutDTOs [i];
                            res = CommonFunctions.InvokeClientSucceed(ref @event, true);
                        }
                        else
                        {
                            throw new Exception("EventPostProcessing _codes_03hSuccess function failed: eventPutDTOs[i].EventNameId eventCode not matched: " + eventPutDTOs[i].EventHex);
                        }
                    }
                    else
                    {
                        throw new Exception("EventPostProcessing _codes_03hSuccess function failed: eventPutDTO eventCode is null: " + eventPutDTOs[i].EventHex);
                    }
                    ++i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EventPostProcessing _codes_03hSuccess function failed: eventPutDTO eventCode is null: " + ex.Message);
            }

            // Assert
            Assert.IsTrue(res);
        }

        
    }
}
