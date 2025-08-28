using VZPStat.EventAsByte;
using VZPStatAPI.FakeData;
using VZPStatAPI.Controllers;
using Common;
using Domain.DataDTO;
using Domain.Models;

namespace TestProject
{
    public class Tests
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
        public void Test_01h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_01h();
            bool res = true;

            // Act
            foreach(var eventAs in eventAsBytes)
            {
                try
                {
                    EventPutDTO eventPutDTO;
                    EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                    eventPutDTO.BranchId = 3;
                    eventPutDTO.FullyProcessed = true;
                    eventPutDTOs.Add(eventPutDTO);
                }
                catch(Exception ex)
                {
                    res = false;
                    Console.WriteLine(ex);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException);
                    }
                }
            }

            // Assert
            if (res)
            {
                Assert.Pass();
            }else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_01h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_01h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach(var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("01"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.PrinterNumber != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.Activity < 250);
                Assert.IsTrue(eventDto.EstimateWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 1);
                ++i;
            }
        }

        [Test]
        public void Test_81h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_81h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_81h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_81h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("81"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.PrinterNumber != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.Activity < 250);
                Assert.IsTrue(eventDto.EstimateWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 2);
                ++i;
            }
        }

        [Test]
        public void Test_41h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_41h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_41h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_41h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("41"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.PrinterNumber != null);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.EstimateWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 3);
                ++i;
            }
        }

        [Test]
        public void Test_C1h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_C1h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_C1h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_C1h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("C1"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.PrinterNumber != null);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.EstimateWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 4);
                ++i;
            }
        }

        [Test]
        public void Test_21h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_21h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_21h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_21h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("21"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.EstimateWaiting == 0);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 5);
                ++i;
            }
        }

        [Test]
        public void Test_A1h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_A1h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_A1h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_A1h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("A1"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.EstimateWaiting == 0);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 6);
                ++i;
            }
        }

        [Test]
        public void Test_02h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_02h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_02h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_02h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("02"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.Activity < 250);
                Assert.IsTrue(eventDto.WaitingTime != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 7);
                ++i;
            }
        }

        [Test]
        public void Test_03h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_03h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_03h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_03h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("03"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.WaitingTime != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 8);
                ++i;
            }
        }

        [Test]
        public void Test_04h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_04h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_04h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_04h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode,
                    Is.EqualTo("04").Or.EqualTo("84").Or.EqualTo("44").Or.EqualTo("24"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.Activity < 250);
                Assert.IsTrue(eventDto.ServiceWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 9);
                ++i;
            }
        }

        [Test]
        public void Test_05h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_05h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_05h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_05h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode,
                    Is.EqualTo("05").Or.EqualTo("85").Or.EqualTo("45").Or.EqualTo("25"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.NewCounter != null);
                Assert.IsTrue(eventDto.ServiceWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 10);
                ++i;
            }
        }

        [Test]
        public void Test_06h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_06h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_06h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_06h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode,
                    Is.EqualTo("06").Or.EqualTo("86").Or.EqualTo("46").Or.EqualTo("26"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.NewActivity != null);
                Assert.IsTrue(eventDto.NewActivity < 250);
                Assert.IsTrue(eventDto.ServiceWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 11);
                ++i;
            }
        }

        [Test]
        public void Test_08h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_08h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_08h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_08h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode,
                    Is.EqualTo("08").Or.EqualTo("88").Or.EqualTo("48").Or.EqualTo("28"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.NewClerk != null);
                Assert.IsTrue(eventDto.ServiceWaiting != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.IsTrue(eventDto.EventNameId == 12);
                ++i;
            }
        }

        [Test]
        public void Test_07h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_07h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_07h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_07h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("07"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(13).Or.EqualTo(14).Or.EqualTo(15).Or.EqualTo(16));
                ++i;
            }
        }

        [Test]
        public void Test_09h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_09h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_09h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_09h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("09"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.PrinterPreviousStateId != null);
                Assert.IsTrue(eventDto.PrinterCurrentStateId != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(17));
                ++i;
            }
        }

        [Test]
        public void Test_0Ah()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Ah();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_0Ah()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Ah();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("0A"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.Activity < 250);
                Assert.IsTrue(eventDto.WaitingTime != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(18));
                ++i;
            }
        }

        [Test]
        public void Test_0Bh()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Bh();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_0Bh()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Bh();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("0B"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                Assert.IsTrue(eventDto.Counter != null);
                Assert.IsTrue(eventDto.Activity != null);
                Assert.IsTrue(eventDto.Activity < 250);
                Assert.IsTrue(eventDto.WaitingTime != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(19));
                ++i;
            }
        }

        [Test]
        public void Test_10h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_10h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_10h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_10h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("10"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(20));
                ++i;
            }
        }

        [Test]
        public void Test_11h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_11h();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_11h()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_11h();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("11"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(21));
                ++i;
            }
        }

        [Test]
        public void Test_0Ch()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Ch();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_0Ch()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Ch();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("0C"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(22).Or.EqualTo(23).Or.EqualTo(24).Or.EqualTo(25).Or.EqualTo(26).Or.EqualTo(27)
                    .Or.EqualTo(28).Or.EqualTo(29).Or.EqualTo(30).Or.EqualTo(31).Or.EqualTo(32));

                if((eventDto.EventNameId == 25) || (eventDto.EventNameId == 26))
                {
                    Assert.IsTrue(eventDto.Counter != null);
                }
                if (eventDto.EventNameId == 27)
                {
                    Assert.IsTrue(eventDto.DiagnosticPin != null);
                }
                if (eventDto.EventNameId == 34)
                {
                    Assert.IsTrue(eventDto.ClientOrdinalNumber > 0);
                }

                ++i;
            }
        }

        [Test]
        public void Test_0Dh()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Dh();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_0Dh()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Dh();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("0D"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.PacketNumOfBytes != null);
                Assert.IsTrue(eventDto.PacketTypeId != null);
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(31));
                ++i;
            }
        }

        [Test]
        public void Test_0Eh()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Eh();
            bool res = true;

            // Act
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

            // Assert
            if (res)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestOutput_0Eh()
        {
            // Arrange
            eventAsBytes = (List<EventAsByte>)faker.GetFakeEvents_0Eh();

            // Act
            foreach (var eventAs in eventAsBytes)
            {
                EventPutDTO eventPutDTO;
                EventProcessing._fromCodeToEventPutDTO(eventAs, out eventPutDTO);
                eventPutDTO.BranchId = 3;
                eventPutDTO.FullyProcessed = true;
                eventPutDTOs.Add(eventPutDTO);
            }

            // Assert
            int i = 0;
            foreach (var eventDto in eventPutDTOs)
            {
                EventPutDTO eventPutDTO = new EventPutDTO();
                eventPutDTO.EventHex = eventAsBytes[i].code;
                eventPutDTO.DateReceived = eventAsBytes[i].date;
                Assert.That(eventDto.EventCode, Is.EqualTo("0E"));
                Assert.That(eventPutDTO.EventHex, Is.EqualTo(eventDto.EventHex));
                Assert.That(eventPutDTO.DateReceived, Is.EqualTo(eventDto.DateReceived));
                Assert.IsTrue(eventDto.BranchId == 3);
                Assert.IsTrue(eventDto.FullyProcessed == true);
                Assert.That(eventDto.EventNameId,
                    Is.EqualTo(32));
                ++i;
            }
        }
    }
}