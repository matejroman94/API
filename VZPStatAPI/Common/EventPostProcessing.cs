using AutoMapper;
using Common.Exceptions;
using Domain.DataDTO;
using Domain.Models;
using Repository.Interfaces;
using VZPStat.EventAsByte;

namespace Common
{
    public class EventPostProcessing
    {
        private readonly IUnitOfWork unitOfWork;
        private IMapper mapper;

        public EventPostProcessing(
            ref IUnitOfWork unitOfWork,
            ref IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool Run(EventAsByte eventAsByte, EventPutDTO eventPutDTO)
        {
            return BytesProcess(ref eventAsByte, eventPutDTO);
        }

        private bool BytesProcess(ref EventAsByte eventAsByte, EventPutDTO eventPutDTO)
        {
            Byte @byte = 0;
            bool res = false;
            @byte = Convert.ToByte(Convert.ToInt32(eventPutDTO.EventCode, 16));
            switch (@byte)
            {
                // Codes 01h, 81h
                case 1:
                case 129:
                    res = _codes_01h_81h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 41h, C1h
                case 65:
                case 193:
                    res = _codes_41h_C1h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 21h, A1h
                case 33:
                case 161:
                    res = _codes_21h_A1h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 02h
                case 2:
                    res = _code_02h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 03h
                case 3:
                    res = _code_03h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes x4h
                case 4:
                case 132:
                case 68:
                case 36:
                    res = _code_x4h(@byte, ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes x5h, x6h, x8h
                case 5:
                case 133:
                case 69:
                case 37:
                case 6:
                case 134:
                case 70:
                case 38:
                case 8:
                case 136:
                case 72:
                case 40:
                    res = _code_x568h(@byte, ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 07h
                case 7:
                    res = _code_07h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 09h
                case 9:
                    res = _codes_09h(ref eventAsByte, ref eventPutDTO);
                    return res;
                // 11h, 12h
                case 16:
                case 17:
                    res = _codes_10_11Ch(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 0Ah, 0Bh
                case 10:
                case 11:
                    res = _codes_0ABh(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 0Ch
                case 12:
                    res = _codes_0Ch(ref eventAsByte, ref eventPutDTO);
                    return res;
                // Codes 0Dh
                case 13:
                    //_codes_0Dh(ref eventAsByte, ref eventPutDTO);
                    return true;
                // Codes 0Eh
                case 14:
                    //_codes_0Eh(ref eventAsByte, ref eventPutDTO);
                    return true;
                default:
                    throw new ControllerExceptionEventAsBytes("Invalid first byte of input code!");
            }
        }

        private bool _codes_01h_81h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 1)
                    {
                        res = NewClientCreateToActivitity(ref eventPutDTO, false);
                    }
                    else if (eventPutDTO.EventNameId == 2)
                    {
                        res = NewClientCreateToActivitity(ref eventPutDTO, true);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_01h_81h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_01h_81h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _codes_41h_C1h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 3)
                    {
                        res = NewClientCreateToCounter(ref eventPutDTO, false);
                    }
                    else if (eventPutDTO.EventNameId == 4)
                    {
                        res = NewClientCreateToCounter(ref eventPutDTO, true);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_41h_C1h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_41h_C1h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _codes_21h_A1h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 5)
                    {
                        res = NewClientCreateToActivityFromCounter(ref eventPutDTO, false);
                    }
                    else if (eventPutDTO.EventNameId == 6)
                    {
                        res = NewClientCreateToActivityFromCounter(ref eventPutDTO, true);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_21h_A1h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_21h_A1h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _code_02h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 7)
                    {
                        res = InvokeClient(ref eventPutDTO, false);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _code_02h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _code_02h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _code_03h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 8)
                    {
                        res = InvokeClient(ref eventPutDTO, true);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _code_03h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _code_03h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _code_x4h(byte @byte, ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 9)
                    {
                        res = FinishClient(ref eventPutDTO);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _code_x4h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _code_x4h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _code_x568h(byte @byte, ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    res = TransferClient(ref eventPutDTO);
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _code_x568h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _code_x568h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _code_07h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 13)
                    {
                        res = SignInClerk(ref eventPutDTO);
                    }
                    else if (eventPutDTO.EventNameId == 14)
                    {
                        res = LogOffClerk(ref eventPutDTO);
                    }
                    else if (eventPutDTO.EventNameId == 15)
                    {
                        res = SignInCounter(ref eventPutDTO);
                    }
                    else if (eventPutDTO.EventNameId == 16)
                    {
                        res = LogOffCounter(ref eventPutDTO);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _code_07h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _code_07h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _codes_09h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 17)
                    {
                        res = ChangePrinterStatus(ref eventPutDTO);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_09h function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_09h function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _codes_10_11Ch(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 20)
                    {
                        res = LogOffVS(ref eventPutDTO);
                    }
                    else if (eventPutDTO.EventNameId == 21)
                    {
                        res = SignInVS(ref eventPutDTO);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_10_11Ch function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_10_11Ch function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _codes_0ABh(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 18 || eventPutDTO.EventNameId == 19)
                    {
                        res = InvokeClient(ref eventPutDTO, false);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_0ABh function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_0ABh function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool _codes_0Ch(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            try
            {
                bool res = false;

                if (eventPutDTO.EventCode is not null)
                {
                    if (eventPutDTO.EventNameId == 22)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 1);
                    }
                    else if (eventPutDTO.EventNameId == 23)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 2);
                    }
                    else if (eventPutDTO.EventNameId == 24)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 3);
                    }
                    else if (eventPutDTO.EventNameId == 25)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 4);
                    }
                    else if (eventPutDTO.EventNameId == 26)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 5);
                    }
                    else if (eventPutDTO.EventNameId == 27)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 6);
                    }
                    else if (eventPutDTO.EventNameId == 28)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 7);
                    }
                    else if (eventPutDTO.EventNameId == 29)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 8);
                    }
                    else if (eventPutDTO.EventNameId == 30)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 9);
                    }
                    else if (eventPutDTO.EventNameId == 31)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 10);
                    }
                    else if (eventPutDTO.EventNameId == 32)
                    {
                        res = AddDiagnosticEventToBranch(ref eventPutDTO, 11);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing _codes_0Ch function failed: EventCode is null: " + eventPutDTO.EventHex, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing _codes_0Ch function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private int? GetPrinterId(EventPutDTO eventPutDTO, int? PrinterNumber, int BranchId)
        {
            try
            {
                if (PrinterNumber is null)
                {
                    //Logger.Logger.NewOperationLog("EventPostProcessing GetPrinterId PrinterNumber is null.", Logger.Logger.Level.Error);
                    return null;
                }

                Printer? printer = unitOfWork.Printers.GetFirstOrDefault(x => x.Number == PrinterNumber
                                && x.BranchId == BranchId);
                if (printer is null)
                {
                    var res = CreateNewPrinter(eventPutDTO, (int)PrinterNumber, BranchId);
                    if(res is true)
                    {
                        return GetPrinterId(eventPutDTO, PrinterNumber, BranchId);
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
                Logger.Logger.NewOperationLog("EventPostProcessing GetPrinterId function failed: " + msg, Logger.Logger.Level.Error);
                return null;
            }
        }

        private int? GetActivityId(EventPutDTO eventPutDTO, int? ActivityNumber, int BranchId)
        {
            try
            {
                if (ActivityNumber is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing GetActivityId ActivityNumber is null.", Logger.Logger.Level.Error);
                    return null;
                }

                Activity? activity = unitOfWork.Activities.GetFirstOrDefault(x => x.Number == ActivityNumber
                                                                            && x.BranchId == BranchId);

                if (activity is null)
                {
                    var res = CreateNewActivity(eventPutDTO, (int)ActivityNumber, BranchId);
                    if (res is true)
                    {
                        return GetActivityId(eventPutDTO, ActivityNumber, BranchId);
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
                Logger.Logger.NewOperationLog("EventPostProcessing GetActivityId function failed: " + msg, Logger.Logger.Level.Error);
                return null;
            }
        }

        private bool CreateNewActivity(EventPutDTO eventPutDTO, int ActivityNumber, int BranchId)
        {
            try
            {
                ActivityPutDTO activityPutDTO = new ActivityPutDTO();
                activityPutDTO.Number = ActivityNumber;
                activityPutDTO.BranchId = BranchId;
                activityPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                if(ActivityNumber == 254) {
                    activityPutDTO.ActivityName = "Mimo frontu";
                } 
                else if (ActivityNumber == 255) {
                    activityPutDTO.ActivityName = "Přeložený z přepážky";
                }

                var entity = mapper.Map<Activity>(activityPutDTO);
                bool res = unitOfWork.Activities.Add(entity);

                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing CreateNewActivity function failed." +
                        "Cannot create activity: " + activityPutDTO.Number, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing CreateNewActivity function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool CreateNewPrinter(EventPutDTO eventPutDTO, int printerNumber, int branchId)
        {
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
                bool res = unitOfWork.Printers.Add(entity);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing CreateNewPrinter function failed." +
                        "Cannot create printer: " + printer.Number, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing CreateNewPrinter function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool NewClientCreateToActivitity(ref EventPutDTO eventPutDTO, bool priority)
        {
            try
            {
                ClientPutDTO clientPutDTO = new ClientPutDTO();
                clientPutDTO.BranchId = eventPutDTO.BranchId;
                clientPutDTO.Priority = priority;
                clientPutDTO.ClientOrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                clientPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                int activity = GetActivityId(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                if(activity == -1)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivitity function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                    return false;
                }
                clientPutDTO.ActivityId = activity;

                clientPutDTO.PrinterId = GetPrinterId(eventPutDTO, eventPutDTO.PrinterNumber, eventPutDTO.BranchId);
                clientPutDTO.ClientStatusId = 1;

                // Check ordinal number
                if (clientPutDTO.ClientOrdinalNumber <= 0) return true;

                var entity = mapper.Map<Client>(clientPutDTO);
                bool res = unitOfWork.Clients.Add(entity);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivitity function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivitity function failed: " + ex.Message +
                            "Cannot create client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool NewClientCreateToCounter(ref EventPutDTO eventPutDTO, bool priority)
        {
            try
            {
                ClientPutDTO clientPutDTO = new ClientPutDTO();
                clientPutDTO.BranchId = eventPutDTO.BranchId;
                clientPutDTO.Priority = priority;
                clientPutDTO.ClientOrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                clientPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                clientPutDTO.CounterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (clientPutDTO.CounterId is null) return false;

                clientPutDTO.PrinterId = GetPrinterId(eventPutDTO, eventPutDTO.PrinterNumber, eventPutDTO.BranchId);
                clientPutDTO.ClientStatusId = 1;

                // Check ordinal number
                if (clientPutDTO.ClientOrdinalNumber <= 0) return true;

                var entity = mapper.Map<Client>(clientPutDTO);
                bool res = unitOfWork.Clients.Add(entity);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToCounter function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToCounter function failed: " + ex.Message +
                            "Cannot create client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool NewClientCreateToActivityFromCounter(ref EventPutDTO eventPutDTO, bool priority)
        {
            try
            {
                ClientPutDTO clientPutDTO = new ClientPutDTO();
                clientPutDTO.BranchId = eventPutDTO.BranchId;
                clientPutDTO.Priority = priority;
                clientPutDTO.ClientOrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                clientPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);

                int activity = GetActivityId(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                if (activity == -1)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivityFromCounter function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                    return false;
                }
                clientPutDTO.ActivityId = activity;

                clientPutDTO.CounterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (clientPutDTO.CounterId is null) return false;
                clientPutDTO.ClientStatusId = 1;

                // Check ordinal number
                if (clientPutDTO.ClientOrdinalNumber <= 0) return true;

                var entity = mapper.Map<Client>(clientPutDTO);
                bool res = unitOfWork.Clients.Add(entity);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivityFromCounter function failed." +
                        "Cannot create client: " + clientPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing NewClientCreateToActivityFromCounter function failed: " + ex.Message +
                            "Cannot create client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                return false;
            }
        }

        private int? GetCounterId(EventPutDTO eventPutDTO, int? CounterNumber, int BranchId)
        {
            try
            {
                if (CounterNumber is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing GetCounterId CounterNumber is null.", Logger.Logger.Level.Error);
                    return null;
                }

                Counter? counter = unitOfWork.Counters.GetFirstOrDefault(x => x.Number == CounterNumber
                                                        && x.BranchId == BranchId);
                if (counter is null)
                {
                    var res = CreateNewCounter(eventPutDTO, (int)CounterNumber, BranchId);
                    if (res is true)
                    {
                        return GetCounterId(eventPutDTO, CounterNumber, BranchId);
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
                Logger.Logger.NewOperationLog("EventPostProcessing GetPrinterId function failed: " + msg, Logger.Logger.Level.Error);
                return null;
            }
        }

        private int? GetClerkIdSignInCounter(int CounterId, int BranchId) {
            try {

                Counter? counter = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                    && x.BranchId == BranchId);
                if (counter is null) {
                    Logger.Logger.NewOperationLog("EventPostProcessing GetClerkIdSignInCounter counter is null."
                        + $"CounterId: {CounterId}, BranchId: {BranchId}"
                        , Logger.Logger.Level.Error);
                    return null;
                }

                return counter.ClerkSignInId;
            }
            catch (Exception ex) {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog($"EventPostProcessing GetClerkIdSignInCounter CounterID: {CounterId} BranchID: {BranchId} function failed: " + msg, Logger.Logger.Level.Error);
                return null;
            }
        }

        private int? GetClerkId(EventPutDTO eventPutDTO,  int? ClerkNumber, int CounterId, int BranchId)
        {
            try
            {
                if (ClerkNumber is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing GetClerkId ClerkNumber is null.", Logger.Logger.Level.Error);
                    return null;
                }

                Counter? counter = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == CounterId 
                    && x.BranchId == BranchId, includeProperties: "Clerks");
                if(counter is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing GetClerkId counter is null."
                        + $"ClerkNumber: {ClerkNumber}, CounterId: {CounterId}, BranchId: {BranchId}"
                        , Logger.Logger.Level.Error);
                    return null;
                }

                Clerk? clerk = counter.Clerks?.FirstOrDefault(x => x.Number == ClerkNumber);
                if (clerk is null)
                {
                    clerk = FindClerk((int)ClerkNumber, BranchId);
                    if(clerk is null)
                    {
                        bool res = CreateNewClerk(eventPutDTO, (int)ClerkNumber, out int ClerkId);
                        if (res is true)
                        {
                            res = JoinClerkToCounter(ClerkId, CounterId, BranchId);
                            if (res is true)
                            {
                                return GetClerkId(eventPutDTO, ClerkNumber, CounterId, BranchId);
                            }
                            else { return null; }
                        }
                        else { return null; }
                    }
                    else
                    {
                        var res = JoinClerkToCounter(clerk.ClerkId, CounterId, BranchId);
                        if (res is true)
                        {
                            return GetClerkId(eventPutDTO, ClerkNumber, CounterId, BranchId);
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
                Logger.Logger.NewOperationLog("EventPostProcessing GetClerkId function failed: " + msg, Logger.Logger.Level.Error);
                return null;
            }
        }

        private bool SetClerkAndCounterStatus(EventPutDTO eventPutDTO, int ClerkId, int CounterId, int StatusId)
        {
            try
            {
                Clerk? clerk = unitOfWork.Clerks.GetFirstOrDefault(x => x.ClerkId == ClerkId, nameof(Clerk.ClerkEvents));
                if (clerk is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetClerkAndCounterStatus cannot get " +
                        $"clerk: ClerkId: {ClerkId}", Logger.Logger.Level.Warning);
                    return false;
                }

                ClerkEventPutDTO clerkEventPutDTO = new ClerkEventPutDTO();
                clerkEventPutDTO.ClerkId = ClerkId;
                clerkEventPutDTO.ClerkStatusId = StatusId;
                clerkEventPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                clerk.ClerkEvents.Add(mapper.Map<ClerkEvent>(clerkEventPutDTO));

                bool res = unitOfWork.Clerks.Update(clerk);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetClerkAndCounterStatus cannot update database" +
                        $"clerk: ClerkId: {ClerkId} " +
                        $"counter: CounterId: {CounterId} " +
                        $"clerkstatus: {StatusId}", Logger.Logger.Level.Warning);
                    return res;
                }

                res = SetCounterStatus(CounterId, StatusId);
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
                Logger.Logger.NewOperationLog("EventPostProcessing SetClerkAndCounterStatus function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool SetCounterStatus(int CounterId, int StatusId)
        {
            try
            {
                Counter? counter = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == CounterId);
                if (counter is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetCounterStatus cannot get " +
                        $"counter: CounterId: {CounterId}", Logger.Logger.Level.Warning);
                    return false;
                }
                counter.CounterStatusId = StatusId;

                bool res = unitOfWork.Counters.UpdateCounterStatus(counter.CounterId, counter.CounterStatusId);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetCounterStatus cannot update database" +
                        $"counter: CounterId: {CounterId}", Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing SetCounterStatus function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool SetPrinterStatus(int PrinterId, int StatusId)
        {
            try
            {
                Printer? printer = unitOfWork.Printers.GetFirstOrDefault(x => x.PrinterId == PrinterId);
                if (printer is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetPrinterStatus cannot get " +
                        $"printer: PrinterId: {PrinterId}", Logger.Logger.Level.Warning);
                    return false;
                }
                printer.PrinterPreviousStateId = printer.PrinterCurrentStateId;
                printer.PrinterCurrentStateId = StatusId;

                bool res = unitOfWork.Printers.UpdatePrinterStatus(printer);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetPrinterStatus cannot update database" +
                        $"printer: PrinterId: {PrinterId}", Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing SetPrinterStatus function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool SetClerkSignInCounter(int ClerkId, int CounterId)
        {
            try
            {
                Clerk? clerk = unitOfWork.Clerks.GetFirstOrDefault(x => x.ClerkId == ClerkId);
                if (clerk is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetClerkSignInCounter cannot get " +
                        $"clerk: ClerkId: {ClerkId}", Logger.Logger.Level.Warning);
                    return false;
                }
                clerk.CounterSignInId = CounterId;

                bool res = unitOfWork.Clerks.Update(clerk);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetClerkSignInCounter cannot update clerk" +
                        $"clerk: ClerkId: {ClerkId}" +
                        $"counter: CounterId: {CounterId}", Logger.Logger.Level.Warning);
                    return res;
                }

                Counter? counter = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == CounterId);
                if (counter is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetClerkSignInCounter cannot get " +
                        $"Counter: CounterID: {CounterId}", Logger.Logger.Level.Warning);
                    return false;
                }
                counter.ClerkSignInId = ClerkId;

                res = unitOfWork.Counters.Update(counter);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing SetClerkSignInCounter cannot update counter" +
                        $"clerk: ClerkId: {ClerkId}" +
                        $"counter: CounterId: {CounterId}", Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing SetClerkSignInCounter function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool CreateNewCounter(EventPutDTO eventPutDTO, int CounterNumber, int BranchId)
        {
            try
            {
                CounterPutDTO counterPutDTO = new CounterPutDTO(CounterNumber, BranchId);
                counterPutDTO.BranchId = BranchId;
                counterPutDTO.Number = CounterNumber;
                counterPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                counterPutDTO.CounterStatusId = 4;

                var entity = mapper.Map<Counter>(counterPutDTO);
                bool res = unitOfWork.Counters.Add(entity);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing CreateNewCounter function failed." +
                        "Cannot create counter: " + counterPutDTO.Number, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing CreateNewCounter function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private Clerk? FindClerk(int ClerkNumber, int BranchId)
        {
            try
            {
                return unitOfWork.Clerks.FindClerkByBranchID(ClerkNumber, BranchId);
            }
            catch (Exception ex) 
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing FindClerk function failed: " + msg, Logger.Logger.Level.Error);
                return null;
            }
        }

        private bool CreateNewClerk(EventPutDTO eventPutDTO, int ClerkNumber, out int ClerkId)
        {
            ClerkId = -1;
            try
            {
                ClerkPutDTO clerkPutDTO = new ClerkPutDTO(ClerkNumber);
                clerkPutDTO.Number = ClerkNumber;
                clerkPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                //clerkPutDTO.ClerkStatusId = 4;

                var clerk = mapper.Map<Clerk>(clerkPutDTO);
                bool res = unitOfWork.Clerks.Add(clerk);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing CreateNewClerk function failed." +
                        $"Cannot add clerk to database: Clerk number: {clerk.Number}", Logger.Logger.Level.Warning);
                    return res;
                }
                ClerkId = clerk.ClerkId;
                ClerkEventPutDTO clerkEventPutDTO = new ClerkEventPutDTO();
                clerkEventPutDTO.ClerkId = ClerkId;
                clerkEventPutDTO.ClerkStatusId = 4;
                clerkEventPutDTO.EventOccurredDate = Common.GetDateTimeFromString(eventPutDTO.DateReceived);
                
                clerk.ClerkEvents.Add(mapper.Map<ClerkEvent>(clerkEventPutDTO));
                res = unitOfWork.Clerks.Update(clerk);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing CreateNewClerk function failed." +
                        $"Cannot add ClerkEvent to database: Clerk number: {clerk.Number}", Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing CreateNewCounter function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool JoinClerkToCounter(int ClerkId, int CounterId, int BranchId)
        {
            try
            {
                var entityCounter = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == CounterId
                        && x.BranchId == BranchId, includeProperties: "Clerks");
                if (entityCounter is null)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing JoinClerkToCounter function failed." +
                                    "Cannot get counter: CounterId: " + CounterId, Logger.Logger.Level.Warning);
                    return false;
                }

                bool res = unitOfWork.Counters.JoinClerkAndCounter(entityCounter.CounterId, ClerkId);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog("EventPostProcessing JoinClerkToCounter function failed." +
                        $"ClerkNumber: {ClerkId}," +
                        $"CounterId: {CounterId}" + 
                        $"BranchId: {BranchId}", Logger.Logger.Level.Warning);
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventPostProcessing JoinClerkToCounter function failed: " + msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool InvokeClient(ref EventPutDTO eventPutDTO, bool WithoutActivity)
        {
            try
            {
                int OrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                var client = GetClientByOrdinalNumber(OrdinalNumber, eventPutDTO.BranchId, eventPutDTO);

                if (client is not null)
                {
                    if(client.ClientStatusId == 2)
                    {
                        client.WaitingTime = eventPutDTO.WaitingTime ?? 0;
                    }
                    else
                    {
                        client.WaitingTime += eventPutDTO.WaitingTime ?? 0;
                    }
                    
                    client.CounterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (client.CounterId is null) return false;

                    if (WithoutActivity == false)
                    {
                        int activity = GetActivityId(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                        if (activity == -1)
                        {
                            Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing InvokeClient function failed." +
                                "Cannot create client: " + client.ClientOrdinalNumber, Logger.Logger.Level.Error);
                            return false;
                        }
                        client.ActivityId = activity;
                    }
                    client.ClientStatusId = 2;
                }
                else { return false; }

                bool res = unitOfWork.Clients.Update(client);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing InvokeClient function failed." +
                        "Cannot update client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing InvokeClient function failed: " + msg +
                        "\nCannot update client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                return false;
            }
        }

        private Client? GetClientByOrdinalNumber(int OrdinalNumber, int BranchID, EventPutDTO eventPutDTO)
        {
            try
            {
                Client? client = null;
                int i = 0;
                while(client is null) {
                    if(i > 5) { break; }
                    client = unitOfWork.Clients.GetFirstOrDefault(x => (x.BranchId == BranchID)
                                               && (x.ClientOrdinalNumber == OrdinalNumber)
                                               && (x.ClientStatusId != 3));
                    if(client is null) { Thread.Sleep(500); }
                    ++i;
                }

                if (client is null)
                {
                    Logger.Logger.NewOperationLog($"Client with EventHex OrdinalNumber and BranchID: {eventPutDTO.EventHex} {OrdinalNumber}, {BranchID}: EventPostProcessing GetClientByOrdinalNumber failed, client cannot be found.", Logger.Logger.Level.Warning);
                    Logger.Logger.NewOperationLog($"{Environment.StackTrace}", Logger.Logger.Level.Warning);
                }

                return client;
            }
            catch (Exception ex)
            {
                Logger.Logger.NewOperationLog($"Client with OrdinalNumber and BranchID: {OrdinalNumber}, {BranchID}: EventPostProcessing GetClientByOrdinalNumber function failed: {ex.Message}", Logger.Logger.Level.Error);
                return null;
            }
        }

        private bool FinishClient(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int OrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                var client = GetClientByOrdinalNumber(OrdinalNumber, eventPutDTO.BranchId, eventPutDTO);

                if (client is not null)
                {
                    client.ServiceWaiting += eventPutDTO.ServiceWaiting ?? 0;
                    client.CounterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (client.CounterId is null) return false;

                    client.ClerkId = GetClerkIdSignInCounter((int)client.CounterId, eventPutDTO.BranchId);
                    if (client.ClerkId < 1) {
                        Logger.Logger.NewOperationLog($"FinishClient GetClerkIdSignInCounter is not set, ClerkID: {client.ClerkId}", Logger.Logger.Level.Warning);
                        client.ClerkId = null;
                    }

                    if (eventPutDTO.Activity is not null)
                    {
                        int activity = GetActivityId(eventPutDTO, eventPutDTO.Activity ?? -1, eventPutDTO.BranchId) ?? -1;
                        if (activity == -1)
                        {
                            Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing FinishClient function failed." +
                                "Cannot create client: " + client.ClientOrdinalNumber, Logger.Logger.Level.Error);
                            return false;
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
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing FinishClient function failed: " + msg +
                        "\nCannot update client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool FinishClient(Client client)
        {
            bool res = unitOfWork.Clients.Update(client);
            if (res is false)
            {
                Logger.Logger.NewOperationLog("EventPostProcessing FinishClient(Client client) function failed." +
                    $"Cannot update client: {client.ClientOrdinalNumber}" , Logger.Logger.Level.Error);
            }
            return res;
        }

        private bool TransferClient(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int OrdinalNumber = eventPutDTO.ClientOrdinalNumber ?? -1;
                var client = GetClientByOrdinalNumber(OrdinalNumber, eventPutDTO.BranchId, eventPutDTO);

                if (client is not null)
                {
                    client.ServiceWaiting += eventPutDTO.ServiceWaiting ?? 0;
                    client.CounterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (client.CounterId is null) return false;

                    client.ClerkId = GetClerkIdSignInCounter((int)client.CounterId, eventPutDTO.BranchId);
                    if (client.ClerkId < 1)
                    {
                        Logger.Logger.NewOperationLog($"TransferClient GetClerkIdSignInCounter is not set, ClerkID: {client.ClerkId}", Logger.Logger.Level.Warning);
                        client.ClerkId = null;
                    }

                    client.TransferReasonId = eventPutDTO.TransferReasonId;
                    client.ClientStatusId = 3;
                    client.ClientDoneId = 3;

                    bool result = FinishClient(client);
                    if(result == false)
                    {
                        Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                                    "FinishClient failed: " + client.ClientOrdinalNumber, Logger.Logger.Level.Error);
                        return false;
                    }

                    if (eventPutDTO.EventNameId == 10)
                    {
                        //Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function EventNameId 10." +
                        //            "FinishClient: " + client.ClientDoneId, Logger.Logger.Level.Warning);
                        eventPutDTO.Counter = eventPutDTO.NewCounter;
                        result = NewClientCreateToCounter(ref eventPutDTO, false);
                        if(result is false)
                        {
                            Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                                    "NewClientCreateToCounter failed: " + client.ClientOrdinalNumber, Logger.Logger.Level.Error);
                            return false;
                        }
                    }
                    else if (eventPutDTO.EventNameId == 11)
                    {
                        if (eventPutDTO.NewActivity is not null)
                        {
                            //Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function EventNameId 11." +
                            //        "FinishClient: " + client.ClientDoneId, Logger.Logger.Level.Warning);
                            eventPutDTO.Activity = eventPutDTO.NewActivity;
                            result = NewClientCreateToActivitity(ref eventPutDTO, false);
                            if (result is false)
                            {
                                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed." +
                                        "NewClientCreateToActivitity failed: " + client.ClientOrdinalNumber, Logger.Logger.Level.Error);
                                return false;
                            }
                        }
                    }
                    else if (eventPutDTO.EventNameId == 12)
                    {
                        //Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function EventNameId 12." +
                        //            "FinishClient: " + client.ClientDoneId, Logger.Logger.Level.Warning);
                        result = NewClientCreateToCounter(ref eventPutDTO, false);
                        if (result is false)
                        {
                            Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient to counter function failed." +
                                    "NewClientCreateToCounter failed: " + client.ClientOrdinalNumber, Logger.Logger.Level.Error);
                            return false;
                        }
                    }
                }
                else { return false; }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing TransferClient function failed: " + msg +
                        "\nCannot update client: " + eventPutDTO.ClientOrdinalNumber, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool SignInClerk(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Warning);
                    return false;
                }

                int? clerkId = GetClerkId(eventPutDTO, eventPutDTO.Clerk, (int)counterId, eventPutDTO.BranchId);
                if (clerkId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                     $"Cannot get clerk ID: ClerkNumber:{eventPutDTO.Clerk}" +
                     $"Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Warning);
                    return false;
                }

                bool res = SetClerkAndCounterStatus(eventPutDTO, (int)clerkId, (int)counterId, 1);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                         $"BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }

                res = SetClerkSignInCounter((int)clerkId, (int)counterId);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                         $"BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInClerk function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool LogOffClerk(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Error);
                    return false;
                }

                int? clerkId = GetClerkId(eventPutDTO, eventPutDTO.Clerk, (int)counterId, eventPutDTO.BranchId);
                if (clerkId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                     $"Cannot get clerk ID: ClerkNumber:{eventPutDTO.Clerk}" +
                     $"Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Error);
                    return false;
                }

                var reasonOfLogOff = eventPutDTO.ReasonSignout ?? 4;
                if (reasonOfLogOff == 255) reasonOfLogOff  = 4;
                bool res = SetClerkAndCounterStatus(eventPutDTO, (int)clerkId, (int)counterId, reasonOfLogOff);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                         $"BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffClerk function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool SignInCounter(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInCounter function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Warning);
                    return false;
                }

                bool res = SetCounterStatus((int)counterId, 1);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInCounter function failed." +
                         $"BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInCounter function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool LogOffCounter(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? counterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                if (counterId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffCounter function failed." +
                     $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Warning);
                    return false;
                }

                var reasonOfLogOff = eventPutDTO.ReasonSignout ?? 4;
                if (reasonOfLogOff == 255) reasonOfLogOff = 4;
                bool res = SetCounterStatus((int)counterId, reasonOfLogOff);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffCounter function failed." +
                         $"BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffCounter function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool ChangePrinterStatus(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int? printerId = GetPrinterId(eventPutDTO, eventPutDTO.PrinterNumber, eventPutDTO.BranchId);
                if (printerId is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing ChangePrinterStatus function failed." +
                     $"Cannot get printer ID: Counter number: {eventPutDTO.PrinterNumber}, BranchId: {eventPutDTO.BranchId}",
                     Logger.Logger.Level.Warning);
                    return false;
                }

                bool res = SetPrinterStatus((int)printerId, eventPutDTO.PrinterCurrentStateId ?? 2);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing ChangePrinterStatus function failed." +
                         $"BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing ChangePrinterStatus function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool SignInVS(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int BranchId = eventPutDTO.BranchId;
                Branch? branch = unitOfWork.Branches.GetFirstOrDefault(x => x.BranchId == BranchId);
                if (branch is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInVS function failed." +
                         $"Cannot get branch by ID. BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return false;
                }

                if (branch.Online == true) return true;

                branch.Online = true;
                bool res = unitOfWork.Branches.Update(branch);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInVS function failed." +
                         $"Update database failed. BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing SignInVS function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool LogOffVS(ref EventPutDTO eventPutDTO)
        {
            try
            {
                int BranchId = eventPutDTO.BranchId;
                Branch? branch = unitOfWork.Branches.GetFirstOrDefault(x => x.BranchId == BranchId);
                if (branch is null)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffVS function failed." +
                         $"Cannot get branch by ID. BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return false;
                }

                if (branch.Online == false) return true;

                branch.Online = false;
                bool res = unitOfWork.Branches.Update(branch);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffVS function failed." +
                         $"Update database failed. BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing LogOffVS function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }

        private bool AddDiagnosticEventToBranch(ref EventPutDTO eventPutDTO, int DiagnosticId)
        {
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
                    int? counterId = GetCounterId(eventPutDTO, eventPutDTO.Counter, eventPutDTO.BranchId);
                    if (counterId is null)
                    {
                        Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing AddDiagnosticEventToBranch function failed." +
                         $"Cannot get counter ID: Counter number: {eventPutDTO.Counter}, BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                        return false;
                    }
                }

                if(!string.IsNullOrWhiteSpace(eventPutDTO.DiagnosticPin))
                {
                    diagnosticBranchPutDTO.Pin = eventPutDTO.DiagnosticPin;
                }

                var entity = mapper.Map<DiagnosticBranch>(diagnosticBranchPutDTO);
                bool res = unitOfWork.DiagnosticBranches.Add(entity);
                if (res is false)
                {
                    Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing AddDiagnosticEventToBranch function failed." +
                         $"Cannot added to database. BranchId: {eventPutDTO.BranchId}",
                         Logger.Logger.Level.Warning);
                    return false;
                }

                return res;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog(eventPutDTO.DateReceived + ": EventPostProcessing AddDiagnosticEventToBranch function failed." +
                       msg, Logger.Logger.Level.Error);
                return false;
            }
        }
    }
}
