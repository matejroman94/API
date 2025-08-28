using AutoMapper;
using Domain.DataDTO;
using Domain.MappingProfiles;
using Domain.Models;
using Moq;
using Repository.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public static class CommonFunctions
    {
        public static bool InvokeClientSucceed(ref EventPutDTO eventPutDTO, bool WithoutActivity)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new DiagnosticBranchProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Update(It.IsAny<Client>()))
              .Returns(true);

            // Act
            try
            {
                int OrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                var client = GetClientByOrdinalNumberSucceed(OrdinalNumber, eventPutDTO.BranchId);

                if (client is not null)
                {
                    if (client.ClientStatusId == 2)
                    {
                        client.WaitingTime = eventPutDTO.WaitingTime ?? 0;
                    }
                    else
                    {
                        client.WaitingTime += eventPutDTO.WaitingTime ?? 0;
                    }

                    client.CounterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (client.CounterId is null) return false;

                    if (WithoutActivity == false)
                    {
                        int activity = GetActivityIdCreateSucceed(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                        if (activity == -1)
                        {
                            throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing InvokeClient function failed." +
                                "Cannot create client: " + client.ClientOrdinalNumber);
                        }
                        client.ActivityId = activity;
                    }
                    client.ClientStatusId = 2;
                }
                else { return false; }

                bool res = mockRepository.Object.Clients.Update(client);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing InvokeClient function failed." +
                        "Cannot update client: " + eventPutDTO.ClientOrdinalNumber);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing InvokeClient function failed: " + ex.Message +
                        "\nCannot update client: " + eventPutDTO.ClientOrdinalNumber);
            }
        }

        public static Client? GetClientByOrdinalNumberSucceed(int OrdinalNumber, int BranchID)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.GetFirstOrDefault(It.IsAny<Expression<Func<Client, bool>>>(),
                It.IsAny<string>(),true))
              .Returns(new Client());

            // Act
            try
            {
                var client = mockRepository.Object.Clients.GetFirstOrDefault(x => x.BranchId == BranchID
                                                                && x.ClientOrdinalNumber == OrdinalNumber
                                                                && x.ClientStatusId != 3);
                if (client is null)
                {
                    throw new Exception($"Client with OrdinalNumber and BranchID: {OrdinalNumber}, {BranchID}: EventPostProcessing GetClientByOrdinalNumber failed, client cannot be found.");
                }
                return client;
            }
            catch (Exception ex)
            {
                throw new Exception($"Client with OrdinalNumber and BranchID: {OrdinalNumber}, {BranchID}: EventPostProcessing GetClientByOrdinalNumber function failed: {ex.Message}");
            }
        }

        public static int? GetCounterIdCreateSucceed(EventPutDTO eventPutDTO, int? CounterNumber, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                It.IsAny<string>(),true))
              .Returns(new Counter
              {
                  CounterId = 1
              });

            // Act
            try
            {
                if (CounterNumber is null)
                {
                    throw new Exception("EventPostProcessing GetCounterId CounterNumber is null.");
                }

                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.Number == CounterNumber
                                        && x.BranchId == BranchId);
                counter = null;
                if (counter is null)
                {
                    var res = CreateNewCounterSucceed(eventPutDTO, (int)CounterNumber, BranchId);
                    if (res is true)
                    {
                        return GetCounterIdNotCreateSucceed(eventPutDTO, CounterNumber, BranchId);
                    }
                    else { return null; }
                }
                else
                {
                    return counter.CounterId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetPrinterId function failed: " + msg);
            }
        }

        public static int? GetCounterIdNotCreateSucceed(EventPutDTO eventPutDTO, int? CounterNumber, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Counter
              {
                  CounterId = 1
              });

            // Act
            try
            {
                if (CounterNumber is null)
                {
                    throw new Exception("EventPostProcessing GetCounterId CounterNumber is null.");
                }

                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.Number == CounterNumber
                                        && x.BranchId == BranchId);
                if (counter is null)
                {
                    var res = CreateNewCounterSucceed(eventPutDTO, (int)CounterNumber, BranchId);
                    if (res is true)
                    {
                        return GetCounterIdNotCreateSucceed(eventPutDTO, CounterNumber, BranchId);
                    }
                    else { return null; }
                }
                else
                {
                    return counter.CounterId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetPrinterId function failed: " + msg);
            }
        }

        public static bool CreateNewCounterSucceed(EventPutDTO eventPutDTO, int CounterNumber, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.Add(It.IsAny<Counter>()))
              .Returns(true);

            // Act
            try
            {
                CounterPutDTO counterPutDTO = new CounterPutDTO(CounterNumber, BranchId);
                counterPutDTO.BranchId = BranchId;
                counterPutDTO.Number = CounterNumber;
                counterPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                counterPutDTO.CounterStatusId = 4;

                var entity = mapper.Map<Counter>(counterPutDTO);
                bool res = mockRepository.Object.Counters.Add(entity);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing CreateNewCounter function failed." +
                        "Cannot create counter: " + counterPutDTO.Number);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing CreateNewCounter function failed: " + msg);
            }
        }

        public static bool NewClientCreateToActivititySucceed(ref EventPutDTO eventPutDTO, bool priority)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Add(It.IsAny<Client>()))
              .Returns(true);

            // Act
            try
            {
                ClientPutDTO clientPutDTO = new ClientPutDTO();
                clientPutDTO.BranchId = eventPutDTO.BranchId;
                clientPutDTO.Priority = priority;
                clientPutDTO.ClientOrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                clientPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                int activity = GetActivityIdCreateSucceed(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                if (activity == -1)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivitity function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                    return false;
                }
                clientPutDTO.ActivityId = activity;

                clientPutDTO.ActivityId = activity;

                clientPutDTO.PrinterId = GetPrinterIdCreateSucceed(eventPutDTO, eventPutDTO.PrinterNumber, eventPutDTO.BranchId);
                clientPutDTO.ClientStatusId = 1;

                var entity = mapper.Map<Client>(clientPutDTO);
                bool res = mockRepository.Object.Clients.Add(entity);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivitity function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivitity function failed: " + ex.Message +
                            "Cannot create client: " + eventPutDTO.ClientOrdinalNumber);
            }
        }

        public static int? GetPrinterIdCreateSucceed(EventPutDTO eventPutDTO, int? PrinterNumber, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Printers.GetFirstOrDefault(It.IsAny<Expression<Func<Printer, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Printer
              {
                  PrinterId = 1
              });

            // Act
            try
            {
                if (PrinterNumber is null)
                {
                    throw new Exception("EventPostProcessing GetPrinterId PrinterNumber is null.");
                }

                Printer? printer = mockRepository.Object.Printers.GetFirstOrDefault(x => x.Number == PrinterNumber
                                                && x.BranchId == BranchId);
                printer = null;
                if (printer is null)
                {
                    var res = CreateNewPrinterSucceed(eventPutDTO, (int)PrinterNumber, BranchId);
                    if (res is true)
                    {
                        return GetPrinterIdNotCreateSucceed(eventPutDTO, PrinterNumber, BranchId);
                    }
                    else { return null; }
                }
                else
                {
                    return printer.PrinterId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetPrinterId function failed: " + msg);
            }
        }

        public static int? GetPrinterIdNotCreateSucceed(EventPutDTO eventPutDTO, int? PrinterNumber, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Printers.GetFirstOrDefault(It.IsAny<Expression<Func<Printer, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Printer
              {
                  PrinterId = 1
              });

            // Act
            try
            {
                if (PrinterNumber is null)
                {
                    throw new Exception("EventPostProcessing GetPrinterId PrinterNumber is null.");
                }

                Printer? printer = mockRepository.Object.Printers.GetFirstOrDefault(x => x.Number == PrinterNumber
                                                && x.BranchId == BranchId);
                if (printer is null)
                {
                    var res = CreateNewPrinterSucceed(eventPutDTO, (int)PrinterNumber, BranchId);
                    if (res is true)
                    {
                        return GetPrinterIdNotCreateSucceed(eventPutDTO, PrinterNumber, BranchId);
                    }
                    else { return null; }
                }
                else
                {
                    return printer.PrinterId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetPrinterId function failed: " + msg);
            }
        }

        public static bool CreateNewPrinterSucceed(EventPutDTO eventPutDTO, int printerNumber, int branchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Printers.Add(It.IsAny<Printer>()))
              .Returns(true);

            // Act
            try
            {
                PrinterPutDTO printer = new PrinterPutDTO();
                printer.BranchId = branchId;
                printer.Number = printerNumber;
                printer.PrinterPreviousStateId = 1;
                printer.PrinterCurrentStateId = 1;
                printer.Number = printerNumber;
                printer.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                var entity = mapper.Map<Printer>(printer);
                bool res = mockRepository.Object.Printers.Add(entity);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing CreateNewPrinter function failed." +
                        "Cannot create printer: " + printer.Number);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing CreateNewPrinter function failed: " + msg);
            }
        }

        public static bool NewClientCreateToCounterSucceed(ref EventPutDTO eventPutDTO, bool priority)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Add(It.IsAny<Client>()))
              .Returns(true);

            // Act
            try
            {
                ClientPutDTO clientPutDTO = new ClientPutDTO();
                clientPutDTO.BranchId = eventPutDTO.BranchId;
                clientPutDTO.Priority = priority;
                clientPutDTO.ClientOrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                clientPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                clientPutDTO.CounterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (clientPutDTO.CounterId is null) return false;

                clientPutDTO.PrinterId = GetPrinterIdCreateSucceed(eventPutDTO, eventPutDTO.PrinterNumber, eventPutDTO.BranchId);
                clientPutDTO.ClientStatusId = 1;

                var entity = mapper.Map<Client>(clientPutDTO);
                bool res = mockRepository.Object.Clients.Add(entity);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToCounter function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToCounter function failed: " + ex.Message +
                            "Cannot create client: " + eventPutDTO.ClientOrdinalNumber);
            }
        }

        public static bool NewClientCreateToActivityFromCounterSucceed(ref EventPutDTO eventPutDTO, bool priority)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Add(It.IsAny<Client>()))
              .Returns(true);

            // Act
            try
            {
                ClientPutDTO clientPutDTO = new ClientPutDTO();
                clientPutDTO.BranchId = eventPutDTO.BranchId;
                clientPutDTO.Priority = priority;
                clientPutDTO.ClientOrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                clientPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                int activity = GetActivityIdCreateSucceed(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                if (activity == -1)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivityFromCounter function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber);
                }
                clientPutDTO.ActivityId = activity;

                clientPutDTO.CounterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (clientPutDTO.CounterId is null) return false;
                clientPutDTO.ClientStatusId = 1;

                var entity = mapper.Map<Client>(clientPutDTO);
                bool res = mockRepository.Object.Clients.Add(entity);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivityFromCounterSucceed function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivityFromCounterSucceed function failed: " + ex.Message +
                            "Cannot create client: " + eventPutDTO.ClientOrdinalNumber);
            }
        }

        public static bool FinishClientSucceed(ref EventPutDTO eventPutDTO)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new DiagnosticBranchProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Update(It.IsAny<Client>()))
              .Returns(true);

            // Act
            try
            {
                int OrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                var client = GetClientByOrdinalNumberSucceed(OrdinalNumber, eventPutDTO.BranchId);

                if (client is not null)
                {
                    client.ServiceWaiting += eventPutDTO.ServiceWaiting ?? 0;
                    client.CounterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (client.CounterId is null) return false;

                    client.ClerkId = GetClerkIdSignInCounterSucceed((int)client.CounterId, eventPutDTO.BranchId);
                    if (client.ClerkId < 1)
                    {
                        throw new Exception($"GetClerkIdSignInCounter is not set, ClerkID: {client.ClerkId}");
                    }

                    if (eventPutDTO.Activity is not null)
                    {
                        int activity = GetActivityIdCreateSucceed(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                        if (activity == -1)
                        {
                            throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing FinishClient function failed." +
                                "Cannot create client: " + client.ClientOrdinalNumber);
                        }
                        client.ActivityId = activity;
                    }
                    client.ClientStatusId = 3;
                    client.ClientDoneId = eventPutDTO.ReasonId;
                }
                else { return false; }

                return FinishClient(client);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing FinishClientSucceed function failed: " + msg +
                        "\nCannot update client: " + eventPutDTO.ClientOrdinalNumber);
            }
        }

        public static bool FinishClient(Client client)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new DiagnosticBranchProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Update(It.IsAny<Client>()))
              .Returns(true);

            // Act
            bool res = mockRepository.Object.Clients.Update(client);
            if (res is false)
            {
                throw new Exception("EventPostProcessing FinishClient(Client client) function failed." +
                    $"Cannot update client: {client.ClientOrdinalNumber}");
            }
            return res;
        }

        public static int? GetClerkIdSignInCounterSucceed(int CounterId, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Counter
              {
                  CounterId = 1,
                  ClerkSignInId = 2
              });

            // Act
            try
            {
                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                    && x.BranchId == BranchId);
                if (counter is null)
                {
                    throw new Exception("EventPostProcessing GetClerkIdSignInCounter counter is null."
                        + $"CounterId: {CounterId}, BranchId: {BranchId}");
                }

                return counter.ClerkSignInId;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetClerkIdSignInCounterSucceed function failed: " + msg);
            }
        }

        public static int? GetClerkIdCreateSucceed(EventPutDTO eventPutDTO, int? ClerkNumber, int CounterId, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Counter
              {
                  CounterId = 1,
              });

            // Act
            try
            {
                if (ClerkNumber is null)
                {
                    throw new Exception("EventPostProcessing GetClerkId ClerkNumber is null.");
                }

                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                    && x.BranchId == BranchId, includeProperties: "Clerks");
                if (counter is null)
                {
                    throw new Exception("EventPostProcessing GetClerkId counter is null."
                        + $"ClerkNumber: {ClerkNumber}, CounterId: {CounterId}, BranchId: {BranchId}"
                        );
                }

                Clerk? clerk = counter.Clerks?.FirstOrDefault(x => x.Number == ClerkNumber);
                if (clerk is null)
                {
                    clerk = FindClerkFailed((int)ClerkNumber, BranchId);
                    if (clerk is null)
                    {
                        bool res = CreateNewClerkSucceed(eventPutDTO, (int)ClerkNumber, out int ClerkId);
                        if (res is true)
                        {
                            res = JoinClerkToCounterSucceed(ClerkId, CounterId, BranchId);
                            if (res is true)
                            {
                                return GetClerkIdClerkFoundSucceed(eventPutDTO, ClerkNumber, CounterId, BranchId);
                            }
                            else { return null; }
                        }
                        else { return null; }
                    }
                    else
                    {
                        var res = JoinClerkToCounterSucceed(clerk.ClerkId, CounterId, BranchId);
                        if (res is true)
                        {
                            return GetClerkIdNotCreateSucceed(eventPutDTO, ClerkNumber, CounterId, BranchId);
                        }
                        else { return null; }
                    }
                }
                else
                {
                    return clerk.ClerkId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetClerkId function failed: " + msg);
            }
        }

        public static int? GetClerkIdClerkFoundSucceed(EventPutDTO eventPutDTO, int? ClerkNumber, int CounterId, int BranchId)
        {
            if (ClerkNumber is null)
            {
                throw new Exception("EventPostProcessing GetClerkId ClerkNumber is null.");
            }

            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Counter
              {
                  CounterId = 1
              });

            // Act
            try
            {
                if (ClerkNumber is null)
                {
                    throw new Exception("EventPostProcessing GetClerkId ClerkNumber is null.");
                }

                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                    && x.BranchId == BranchId, includeProperties: "Clerks");
                if (counter is null)
                {
                    throw new Exception("EventPostProcessing GetClerkId counter is null."
                        + $"ClerkNumber: {ClerkNumber}, CounterId: {CounterId}, BranchId: {BranchId}"
                        );
                }

                Clerk? clerk = counter.Clerks?.FirstOrDefault(x => x.Number == ClerkNumber);
                if (clerk is null)
                {
                    clerk = FindClerkSucceed((int)ClerkNumber, BranchId);
                    if (clerk is null)
                    {
                        bool res = CreateNewClerkSucceed(eventPutDTO, (int)ClerkNumber, out int ClerkId);
                        if (res is true)
                        {
                            res = JoinClerkToCounterSucceed(ClerkId, CounterId, BranchId);
                            if (res is true)
                            {
                                return GetClerkIdClerkFoundSucceed(eventPutDTO, ClerkNumber, CounterId, BranchId);
                            }
                            else { return null; }
                        }
                        else { return null; }
                    }
                    else
                    {
                        var res = JoinClerkToCounterSucceed(clerk.ClerkId, CounterId, BranchId);
                        if (res is true)
                        {
                            return GetClerkIdNotCreateSucceed(eventPutDTO, ClerkNumber, CounterId, BranchId);
                        }
                        else { return null; }
                    }
                }
                else
                {
                    return clerk.ClerkId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetClerkId function failed: " + msg);
            }
        }

        public static int? GetClerkIdNotCreateSucceed(EventPutDTO eventPutDTO, int? ClerkNumber, int CounterId, int BranchId)
        {
            if (ClerkNumber is null)
            {
                throw new Exception("EventPostProcessing GetClerkId ClerkNumber is null.");
            }

            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Counter
              {
                  CounterId = 1,
                  Clerks = new List<Clerk> { new Clerk { ClerkId = 1, Number = (int)ClerkNumber } }
              });

            // Act
            try
            {
                if (ClerkNumber is null)
                {
                    throw new Exception("EventPostProcessing GetClerkId ClerkNumber is null.");
                }

                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                    && x.BranchId == BranchId, includeProperties: "Clerks");
                if (counter is null)
                {
                    throw new Exception("EventPostProcessing GetClerkId counter is null."
                        + $"ClerkNumber: {ClerkNumber}, CounterId: {CounterId}, BranchId: {BranchId}"
                        );
                }

                Clerk? clerk = counter.Clerks?.FirstOrDefault(x => x.Number == ClerkNumber);
                if (clerk is null)
                {
                    clerk = FindClerkFailed((int)ClerkNumber, BranchId);
                    if (clerk is null)
                    {
                        bool res = CreateNewClerkSucceed(eventPutDTO, (int)ClerkNumber, out int ClerkId);
                        if (res is true)
                        {
                            res = JoinClerkToCounterSucceed(ClerkId, CounterId, BranchId);
                            if (res is true)
                            {
                                return GetClerkIdClerkFoundSucceed(eventPutDTO, ClerkNumber, CounterId, BranchId);
                            }
                            else { return null; }
                        }
                        else { return null; }
                    }
                    else
                    {
                        var res = JoinClerkToCounterSucceed(clerk.ClerkId, CounterId, BranchId);
                        if (res is true)
                        {
                            return GetClerkIdNotCreateSucceed(eventPutDTO,ClerkNumber, CounterId, BranchId);
                        }
                        else { return null; }
                    }
                }
                else
                {
                    return clerk.ClerkId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetClerkId function failed: " + msg);
            }
        }

        public static bool CreateNewClerkSucceed(EventPutDTO eventPutDTO, int ClerkNumber, out int ClerkId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new ClerkEventProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clerks.Add(It.IsAny<Clerk>()))
                .Returns(true);
            mockRepository.Setup(p => p.Clerks.Update(It.IsAny<Clerk>()))
                .Returns(true);

            // Act
            ClerkId = -1;
            try
            {
                ClerkPutDTO clerkPutDTO = new ClerkPutDTO(ClerkNumber);
                clerkPutDTO.Number = ClerkNumber;
                clerkPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                var clerk = mapper.Map<Clerk>(clerkPutDTO);
                bool res = mockRepository.Object.Clerks.Add(clerk);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing CreateNewClerk function failed." +
                        $"Cannot add clerk to database: Clerk number: {clerk.Number}");
                }
                ClerkId = clerk.ClerkId;
                ClerkEventPutDTO clerkEventPutDTO = new ClerkEventPutDTO();
                clerkEventPutDTO.ClerkId = ClerkId;
                clerkEventPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                clerkEventPutDTO.ClerkStatusId = 4;

                clerk.ClerkEvents.Add(mapper.Map<ClerkEvent>(clerkEventPutDTO));
                res = mockRepository.Object.Clerks.Update(clerk);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing CreateNewClerk function failed." +
                        $"Cannot add ClerkEvent to database: Clerk number: {clerk.Number}");
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing CreateNewCounter function failed: " + msg);
            }
        }

        public static bool JoinClerkToCounterSucceed(int ClerkId, int CounterId, int BranchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                            It.IsAny<string>(), true))
                          .Returns(new Counter
                          {
                              CounterId = 1,
                              Clerks = new List<Clerk> { new Clerk { ClerkId = 1 } }
                          });

            mockRepository.Setup(p => p.Counters.JoinClerkAndCounter(It.IsAny<int>(),
                It.IsAny<int>()))
              .Returns(true);

            // Act
            try
            {
                var entityCounter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                        && x.BranchId == BranchId, includeProperties: "Clerks");
                if (entityCounter is null)
                {
                    throw new Exception("EventPostProcessing JoinClerkToCounter function failed." +
                                    "Cannot get counter: CounterId: " + CounterId);
                }

                bool res = mockRepository.Object.Counters.JoinClerkAndCounter(entityCounter.CounterId, ClerkId);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing JoinClerkToCounter function failed." +
                        $"ClerkNumber: {ClerkId}," +
                        $"CounterId: {CounterId}" +
                        $"BranchId: {BranchId}");
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing JoinClerkToCounter function failed: " + msg);
            }
        }

        public static Clerk? FindClerkSucceed(int ClerkNumber, int BranchId)
        {
            try
            {
                return new Clerk
                {
                    ClerkId = 1,
                    Number = ClerkNumber
                };
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing FindClerk function failed: " + msg);
            }
        }

        public static Clerk? FindClerkFailed(int ClerkNumber, int BranchId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing FindClerk function failed: " + msg);
            }
        }

        public static bool TransferClientSucceed(ref EventPutDTO eventPutDTO)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new DiagnosticBranchProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clients.Update(It.IsAny<Client>()))
              .Returns(true);

            // Act
            try
            {
                int OrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                var client = GetClientByOrdinalNumberSucceed(OrdinalNumber, eventPutDTO.BranchId);

                if (client is not null)
                {
                    client.ServiceWaiting += eventPutDTO.ServiceWaiting ?? 0;
                    client.CounterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (client.CounterId is null) return false;

                    client.ClerkId = GetClerkIdSignInCounterSucceed((int)client.CounterId, eventPutDTO.BranchId);
                    if (client.ClerkId < 1)
                    {
                        throw new Exception($"TransferClientSucceed GetClerkIdSignInCounter is not set, ClerkID: {client.ClerkId}");
                    }

                    client.TransferReasonId = eventPutDTO.TransferReasonId;
                    client.ClientStatusId = 3;
                    client.ClientDoneId = 3;

                    bool result = FinishClient(client);
                    if (result == false)
                    {
                        throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                                    "FinishClient failed: " + client.ClientOrdinalNumber);
                    }

                    if (eventPutDTO.EventNameId == 10)
                    {
                        eventPutDTO.Counter = eventPutDTO.NewCounter;
                        result = NewClientCreateToCounterSucceed(ref eventPutDTO, false);
                        if (result is false)
                        {
                            throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                                    "NewClientCreateToCounterSucceed failed: " + client.ClientOrdinalNumber);
                        }
                    }
                    else if (eventPutDTO.EventNameId == 11)
                    {
                        if (eventPutDTO.NewActivity is not null)
                        {
                            eventPutDTO.Activity = eventPutDTO.NewActivity;
                            result = NewClientCreateToActivititySucceed(ref eventPutDTO, false);
                            if (result is false)
                            {
                                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                                    "NewClientCreateToActivititySucceed failed: " + client.ClientOrdinalNumber);
                            }
                        }
                    }
                    else if (eventPutDTO.EventNameId == 12)
                    {
                        client.ClerkId = GetClerkIdCreateSucceed(eventPutDTO, eventPutDTO.NewClerk, (int)client.CounterId, eventPutDTO.BranchId);
                    }

                    client.ClientStatusId = 1;
                    client.ClientDoneId = eventPutDTO.ReasonId;
                }
                else { return false; }

                bool res = mockRepository.Object.Clients.Update(client);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                        "Cannot update client: " + eventPutDTO.ClientOrdinalNumber);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed: " + msg +
                        "\nCannot update client: " + eventPutDTO.ClientOrdinalNumber);
            }
        }

        public static bool SignInClerkSucceed(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                }

                int? clerkId = GetClerkIdCreateSucceed(eventPutDTO, eventPutDTO.Clerk, (int)counterId, eventPutDTO.BranchId);
                if (clerkId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                     $"Cannot get clerk ID: ClerkNumber:{eventPutDTO.Clerk}" +
                     $"Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                }

                bool res = SetClerkAndCounterStatusSucceed(eventPutDTO, (int)clerkId, (int)counterId, 1);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                         $"BranchId: {eventPutDTO.BranchId}");
                }

                res = SetClerkSignInCounterSucceed((int)clerkId, (int)counterId);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                         $"BranchId: {eventPutDTO.BranchId}");
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                       msg);
            }
        }

        public static bool SetClerkAndCounterStatusSucceed(EventPutDTO eventPutDTO, int ClerkId, int CounterId, int StatusId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new ClerkEventProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clerks.GetFirstOrDefault(It.IsAny<Expression<Func<Clerk, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Clerk { ClerkId = 1});
            mockRepository.Setup(p => p.Clerks.UpdateClerkStatus(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(true);

            // Act
            try
            {
                Clerk? clerk = mockRepository.Object.Clerks.GetFirstOrDefault(x => x.ClerkId == ClerkId);
                if (clerk is null)
                {
                    throw new Exception("EventPostProcessing SetClerkAndCounterStatus cannot get " +
                        $"clerk: ClerkId: {ClerkId}");
                }

                ClerkEventPutDTO clerkEventPutDTO = new ClerkEventPutDTO();
                clerkEventPutDTO.ClerkId = ClerkId;
                clerkEventPutDTO.ClerkStatusId = StatusId;
                clerkEventPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                clerk.ClerkEvents.Add(mapper.Map<ClerkEvent>(clerkEventPutDTO));

                bool res = mockRepository.Object.Clerks.UpdateClerkStatus(clerk.ClerkId, StatusId);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing SetClerkAndCounterStatus cannot update database" +
                        $"clerk: ClerkId: {ClerkId}" +
                        $"counter: CounterId: {CounterId}");
                }

                res = SetCounterStatusSucceed(CounterId, StatusId);
                if (res is false)
                {
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing SetClerkAndCounterStatus function failed." +
                       msg);
            }
        }

        public static bool SetClerkSignInCounterSucceed(int ClerkId, int CounterId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Clerks.GetFirstOrDefault(It.IsAny<Expression<Func<Clerk, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Clerk { ClerkId = 1 });
            mockRepository.Setup(p => p.Clerks.Update(It.IsAny<Clerk>()))
              .Returns(true);

            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Counter { CounterId = 1 });
            mockRepository.Setup(p => p.Counters.Update(It.IsAny<Counter>()))
              .Returns(true);

            // Act
            try
            {
                Clerk? clerk = mockRepository.Object.Clerks.GetFirstOrDefault(x => x.ClerkId == ClerkId);
                if (clerk is null)
                {
                    throw new Exception("EventPostProcessing SetClerkSignInCounter cannot get " +
                        $"clerk: ClerkId: {ClerkId}");
                }
                clerk.CounterSignInId = CounterId;

                bool res = mockRepository.Object.Clerks.Update(clerk);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing SetClerkSignInCounter cannot update database" +
                        $"clerk: ClerkId: {ClerkId}" +
                        $"counter: CounterId: {CounterId}");
                }

                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId);
                if (counter is null)
                {
                    throw new Exception("EventPostProcessing SetClerkSignInCounter cannot get " +
                        $"Counter: CounterID: {CounterId}");
                }
                counter.ClerkSignInId = ClerkId;

                res = mockRepository.Object.Counters.Update(counter);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing SetClerkSignInCounter cannot update counter" +
                        $"clerk: ClerkId: {ClerkId}" +
                        $"counter: CounterId: {CounterId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing SetClerkSignInCounter function failed." +
                       msg);
            }
        }

        public static bool LogOffClerkSucceed(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                }

                int? clerkId = GetClerkIdCreateSucceed(eventPutDTO, eventPutDTO.Clerk, (int)counterId, eventPutDTO.BranchId);
                if (clerkId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                     $"Cannot get clerk ID: ClerkNumber:{eventPutDTO.Clerk}" +
                     $"Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                }

                var reasonOfLogOff = eventPutDTO.ReasonSignout ?? 4;
                if (reasonOfLogOff == 255) reasonOfLogOff = 4;
                bool res = SetClerkAndCounterStatusSucceed(eventPutDTO, (int)clerkId, (int)counterId, reasonOfLogOff);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                         $"BranchId: {eventPutDTO.BranchId}");
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                       msg);
            }
        }

        public static bool SetCounterStatusSucceed(int CounterId, int StatusId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Counters.GetFirstOrDefault(It.IsAny<Expression<Func<Counter, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Counter { CounterId = 1 });
            mockRepository.Setup(p => p.Counters.UpdateCounterStatus(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            try
            {
                Counter? counter = mockRepository.Object.Counters.GetFirstOrDefault(x => x.CounterId == CounterId);
                if (counter is null)
                {
                    throw new Exception("EventPostProcessing SetCounterStatus cannot get " +
                        $"counter: CounterId: {CounterId}");
                }
                counter.CounterStatusId = StatusId;

                bool res = mockRepository.Object.Counters.UpdateCounterStatus(counter.CounterId, counter.CounterStatusId);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing SetCounterStatus cannot update database" +
                        $"counter: CounterId: {CounterId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing SetCounterStatus function failed." +
                       msg);
            }
        }

        public static bool SignInCounterSucceed(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInCounter function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                }

                bool res = SetCounterStatusSucceed((int)counterId, 1);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInCounter function failed." +
                         $"BranchId: {eventPutDTO.BranchId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInCounter function failed." +
                       msg);
            }
        }

        public static bool LogOffCounterSucceed(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffCounter function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                }

                var reasonOfLogOff = eventPutDTO.ReasonSignout ?? 4;
                if (reasonOfLogOff == 255) reasonOfLogOff = 4;
                bool res = SetCounterStatusSucceed((int)counterId, reasonOfLogOff);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffCounter function failed." +
                         $"BranchId: {eventPutDTO.BranchId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffCounter function failed." +
                       msg);
            }
        }

        public static bool ChangePrinterStatusSucceed(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? printerId = GetPrinterIdCreateSucceed(eventPutDTO, eventPutDTO.PrinterNumber, eventPutDTO.BranchId);
                if (printerId is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing ChangePrinterStatus function failed." +
                     $"Cannot get printer ID: Counter number: {eventPutDTO.PrinterNumber} BranchId: {eventPutDTO.BranchId}");
                }

                bool res = SetPrinterStatusSucceed((int)printerId, eventPutDTO.PrinterCurrentStateId ?? 2);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing ChangePrinterStatus function failed." +
                         $"BranchId: {eventPutDTO.BranchId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing ChangePrinterStatus function failed.");
            }
        }

        public static bool SetPrinterStatusSucceed(int PrinterId, int StatusId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Printers.GetFirstOrDefault(It.IsAny<Expression<Func<Printer, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Printer { PrinterId = 1, PrinterPreviousStateId = 1 });
            mockRepository.Setup(p => p.Printers.UpdatePrinterStatus(It.IsAny<Printer>()))
                .Returns(true);

            // Act
            try
            {
                Printer? printer = mockRepository.Object.Printers.GetFirstOrDefault(x => x.PrinterId == PrinterId);
                if (printer is null)
                {
                    throw new Exception("EventPostProcessing SetPrinterStatus cannot get " +
                        $"printer: PrinterId: {PrinterId}");
                }
                printer.PrinterPreviousStateId = printer.PrinterCurrentStateId;
                printer.PrinterCurrentStateId = StatusId;

                bool res = mockRepository.Object.Printers.UpdatePrinterStatus(printer);
                if (res is false)
                {
                    throw new Exception("EventPostProcessing SetPrinterStatus cannot update database" +
                        $"printer: PrinterId: {PrinterId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing SetPrinterStatus function failed." +
                       msg);
            }
        }

        public static bool SignInVSSucceed(ref EventPutDTO eventPutDTO)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Branches.GetFirstOrDefault(It.IsAny<Expression<Func<Branch, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Branch { BranchId = 1, Online = false });
            mockRepository.Setup(p => p.Branches.Update(It.IsAny<Branch>()))
                .Returns(true);

            // Act
            try
            {
                int BranchId = eventPutDTO.BranchId;
                Branch? branch = mockRepository.Object.Branches.GetFirstOrDefault(x => x.BranchId == BranchId);
                if (branch is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInVS function failed." +
                         $"Cannot get branch by ID. BranchId: {eventPutDTO.BranchId}");
                }

                if (branch.Online == true) return true;

                branch.Online = true;
                bool res = mockRepository.Object.Branches.Update(branch);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInVS function failed." +
                         $"Update database failed. BranchId: {eventPutDTO.BranchId}");
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing SignInVS function failed." +
                        msg);
            }
        }

        public static bool LogOffVS(ref EventPutDTO eventPutDTO)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Branches.GetFirstOrDefault(It.IsAny<Expression<Func<Branch, bool>>>(),
                    It.IsAny<string>(), true))
                  .Returns(new Branch { BranchId = 1, Online = true });
            mockRepository.Setup(p => p.Branches.Update(It.IsAny<Branch>()))
                .Returns(true);

            // Act
            try
            {
                int BranchId = eventPutDTO.BranchId;
                Branch? branch = mockRepository.Object.Branches.GetFirstOrDefault(x => x.BranchId == BranchId);
                if (branch is null)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffVS function failed." +
                         $"Cannot get branch by ID. BranchId: {eventPutDTO.BranchId}");
                }

                if (branch.Online == false) return true;

                branch.Online = false;
                bool res = mockRepository.Object.Branches.Update(branch);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffVS function failed." +
                         $"Update database failed. BranchId: {eventPutDTO.BranchId}");
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing LogOffVS function failed." +
                       msg);
            }
        }

        public static bool AddDiagnosticEventToBranchSucceed(ref EventPutDTO eventPutDTO, int DiagnosticId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BranchProfile());
                cfg.AddProfile(new ClerkProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new CounterProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new LoggerProfile());
                cfg.AddProfile(new PrinterProfile());
                cfg.AddProfile(new DiagnosticBranchProfile());

            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.DiagnosticBranches.Add(It.IsAny<DiagnosticBranch>()))
                .Returns(true);

            // Act
            try
            {
                int BranchId = eventPutDTO.BranchId;

                DiagnosticBranchPutDTO diagnosticBranchPutDTO = new DiagnosticBranchPutDTO();
                diagnosticBranchPutDTO.BranchId = BranchId;
                diagnosticBranchPutDTO.DiagnosticId = DiagnosticId;
                diagnosticBranchPutDTO.PeriphTypeId = eventPutDTO.PeriphTypeId;
                diagnosticBranchPutDTO.PeriphNumber = eventPutDTO.DiagnosticPeriphOrdinalNumber;
                diagnosticBranchPutDTO.DiagnosticData = eventPutDTO.DiagnosticData;
                diagnosticBranchPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                if (eventPutDTO.Counter is not null)
                {
                    int? counterId = GetCounterIdCreateSucceed(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (counterId is null)
                    {
                        throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing AddDiagnosticEventToBranch function failed." +
                         $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(eventPutDTO.DiagnosticPin))
                {
                    diagnosticBranchPutDTO.Pin = eventPutDTO.DiagnosticPin;
                }

                var entity = mapper.Map<DiagnosticBranch>(diagnosticBranchPutDTO);
                bool res = mockRepository.Object.DiagnosticBranches.Add(entity);
                if (res is false)
                {
                    throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing AddDiagnosticEventToBranch function failed." +
                         $"Cannot added to database. BranchId: {eventPutDTO.BranchId}");
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception(eventPutDTO.DateReceived + ": EventPostProcessing AddDiagnosticEventToBranch function failed." +
                       msg);
            }
        }

        public static int? GetActivityIdCreateSucceed(EventPutDTO eventPutDTO, int? ActivityNumber, int BranchId)
        {
            if (ActivityNumber is null)
            {
                throw new Exception("EventPostProcessing GetActivityId ActivityNumber is null.");
            }

            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Activities.GetFirstOrDefault(It.IsAny<Expression<Func<Activity, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Activity
              {
                  ActivityId = 1
              });

            //Act
            try
            {
                if (ActivityNumber is null)
                {
                    throw new Exception("EventPostProcessing GetActivityId ActivityNumber is null.");
                }

                Activity? activity = mockRepository.Object.Activities.GetFirstOrDefault(x => x.Number == ActivityNumber
                                                                            && x.BranchId == BranchId);

                if (activity is null)
                {
                    var res = CreateNewActivitySucceed(eventPutDTO, (int)ActivityNumber, BranchId);
                    if (res is true)
                    {
                        return GetActivityIdNotCreateSucceed(eventPutDTO, ActivityNumber, BranchId);
                    }
                    else { return null; }
                }
                else
                {
                    return activity.ActivityId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetActivityId function failed: " + msg);
            }
        }

        public static bool CreateNewActivitySucceed(EventPutDTO eventPutDTO, int activityNumber, int branchId)
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Activities.Add(It.IsAny<Activity>()))
              .Returns(true);

            //Act
            try
            {
                ActivityPutDTO activityPutDTO = new ActivityPutDTO();
                activityPutDTO.Number = activityNumber;
                activityPutDTO.BranchId = branchId;
                activityPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                if (activityNumber == 254) {
                    activityPutDTO.ActivityName = "Mimo frontu";
                }
                else if (activityNumber == 255) {
                    activityPutDTO.ActivityName = "Přeložený z přepážky";
                }

                var entity = mapper.Map<Activity>(activityPutDTO);
                bool res = mockRepository.Object.Activities.Add(entity);

                if (res is false)
                {
                    throw new Exception("EventPostProcessing CreateNewActivity function failed." +
                        "Cannot create activity: " + activityPutDTO.Number);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing CreateNewActivity function failed: " + msg);
            }
        }

        public static int? GetActivityIdNotCreateSucceed(EventPutDTO eventPutDTO, int? ActivityNumber, int BranchId)
        {
            if (ActivityNumber is null)
            {
                throw new Exception("EventPostProcessing GetActivityId ActivityNumber is null.");
            }

            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ActivityProfile());
            });
            var mapper = config.CreateMapper();
            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(p => p.Activities.GetFirstOrDefault(It.IsAny<Expression<Func<Activity, bool>>>(),
                It.IsAny<string>(), true))
              .Returns(new Activity
              {
                  ActivityId = 1, 
                  Number = (int)ActivityNumber 
              });

            //Act
            try
            {
                if (ActivityNumber is null)
                {
                    throw new Exception("EventPostProcessing GetActivityId ActivityNumber is null.");
                }

                Activity? activity = mockRepository.Object.Activities.GetFirstOrDefault(x => x.Number == ActivityNumber
                                                                            && x.BranchId == BranchId);

                if (activity is null)
                {
                    var res = CreateNewActivitySucceed(eventPutDTO, (int)ActivityNumber, BranchId);
                    if (res is true)
                    {
                        return GetActivityIdNotCreateSucceed(eventPutDTO, ActivityNumber, BranchId);
                    }
                    else { return null; }
                }
                else
                {
                    return activity.ActivityId;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                throw new Exception("EventPostProcessing GetActivityId function failed: " + msg);
            }
        }
    }
}
