using Common.Exceptions;
using Domain.DataDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZPStat.EventAsByte;

namespace Common
{
    public static class EventProcessing
    {
        public static void _fromCodeToEventPutDTO(EventAsByte eventAsByte, out EventPutDTO eventPutDTO)
        {
            eventPutDTO = new EventPutDTO();
            eventPutDTO.EventCode = Common.ReturnByteAsStringFromCode(eventAsByte.code, 0);
            BytesProcess(ref eventAsByte, ref eventPutDTO);
        }

        public static void BytesProcess(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            Byte @byte = 0;
            @byte = Convert.ToByte(Convert.ToInt32(eventPutDTO.EventCode, 16));
            switch (@byte)
            {
                // Codes 01h, 81h
                case 1:
                case 129:
                    _code_01h_81h(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 41h, C1h
                case 65:
                case 193:
                    _code_41h_C1h(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 21h, A1h
                case 33:
                case 161:
                    _code_21h_A1h(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 02h
                case 2:
                    _code_02h(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 03h
                case 3:
                    _code_03h(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes x4h
                case 4:
                case 132:
                case 68:
                case 36:
                    _code_x4h(@byte, ref eventAsByte, ref eventPutDTO);
                    break;
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
                    _code_x568h(@byte, ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 07h
                case 7:
                    _code_07h(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 09h
                case 9:
                    _code_09h(ref eventAsByte, ref eventPutDTO);
                    break;
                // 11h, 12h
                case 16:
                case 17:
                    _code_10_11Ch(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 0Ah, 0Bh
                case 10:
                case 11:
                    _code_0ABh(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 0Ch
                case 12:
                    _code_0Ch(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 0Dh
                case 13:
                    _code_0Dh(ref eventAsByte, ref eventPutDTO);
                    break;
                // Codes 0Eh
                case 14:
                    _code_0Eh(ref eventAsByte, ref eventPutDTO);
                    break;
                default:
                    throw new ControllerExceptionEventAsBytes("Invalid first byte of input code!");
            }
        }

        public static void _code_01h_81h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("01"))
                {
                    eventPutDTO.EventNameId = 1;
                }
                else if (eventPutDTO.EventCode.Equals("81"))
                {
                    eventPutDTO.EventNameId = 2;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.PrinterNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            eventPutDTO.Activity = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
            });
            eventPutDTO.EstimateWaiting = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_41h_C1h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("41"))
                {
                    eventPutDTO.EventNameId = 3;
                }
                else if (eventPutDTO.EventCode.Equals("C1"))
                {
                    eventPutDTO.EventNameId = 4;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.PrinterNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
            });
            eventPutDTO.EstimateWaiting = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_21h_A1h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("21"))
                {
                    eventPutDTO.EventNameId = 5;
                }
                else if (eventPutDTO.EventCode.Equals("A1"))
                {
                    eventPutDTO.EventNameId = 6;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            eventPutDTO.Activity = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
            });
            eventPutDTO.EstimateWaiting = 0;
        }

        public static void _code_02h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("02"))
                {
                    eventPutDTO.EventNameId = 7;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            eventPutDTO.Activity = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
            });
            eventPutDTO.WaitingTime = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_03h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);

            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("03"))
                {
                    eventPutDTO.EventNameId = 8;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            eventPutDTO.Activity = null;
            eventPutDTO.WaitingTime = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_x4h(byte @byte, ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            switch (@byte)
            {
                case 132:
                    eventPutDTO.ReasonId = 1;
                    break;
                case 68:
                    eventPutDTO.ReasonId = 2;
                    break;
                case 4:
                case 36:
                    eventPutDTO.ReasonId = 3;
                    break;
                default:
                    break;
            }
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("04") || eventPutDTO.EventCode.Equals("84") || eventPutDTO.EventCode.Equals("44") || eventPutDTO.EventCode.Equals("24"))
                {
                    eventPutDTO.EventNameId = 9;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });


            eventPutDTO.Activity = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                        Convert.ToByte(Convert.ToInt32(P3,16)),
            });

            eventPutDTO.ServiceWaiting = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_x568h(byte @byte, ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            switch (@byte)
            {
                case 133:
                case 134:
                case 136:
                    eventPutDTO.TransferReasonId = 1; // Priority
                    break;
                case 69:
                case 70:
                case 72:
                    eventPutDTO.TransferReasonId = 2; // Prelozeni na cas
                    break;
                case 37:
                case 38:
                case 40:
                    eventPutDTO.TransferReasonId = 3; // Zpracovani klienta
                    break;
            }
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("05") || eventPutDTO.EventCode.Equals("85") || eventPutDTO.EventCode.Equals("45") || eventPutDTO.EventCode.Equals("25"))
                {
                    eventPutDTO.EventNameId = 10;
                }
                else if (eventPutDTO.EventCode.Equals("06") || eventPutDTO.EventCode.Equals("86") || eventPutDTO.EventCode.Equals("46") || eventPutDTO.EventCode.Equals("26"))
                {
                    eventPutDTO.EventNameId = 11;
                }
                else if (eventPutDTO.EventCode.Equals("08") || eventPutDTO.EventCode.Equals("88") || eventPutDTO.EventCode.Equals("48") || eventPutDTO.EventCode.Equals("28"))
                {
                    eventPutDTO.EventNameId = 12;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            @byte &= 15;
            if (@byte == 5)
            {
                eventPutDTO.NewCounter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
                });
            }
            else if (@byte == 6)
            {
                eventPutDTO.NewActivity = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
                });
            }
            else if (@byte == 8)
            {
                eventPutDTO.NewClerk = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
                });
            }

            eventPutDTO.ServiceWaiting = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_07h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
               Convert.ToByte(Convert.ToInt32(P2,16))
            });
            if (Convert.ToInt32(P0, 16) == 255)
            {
                if (Convert.ToInt32(P1, 16) != 254)
                {
                    if (eventPutDTO.EventCode is not null)
                    {
                        eventPutDTO.EventNameId = 13;
                    }
                    eventPutDTO.Clerk = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                    {
                            Convert.ToByte(Convert.ToInt32(P1,16))
                    });
                }
                else
                {
                    if (eventPutDTO.EventCode is not null)
                    {
                        eventPutDTO.EventNameId = 15;
                    }
                }
            }
            else if (Convert.ToInt32(P1, 16) == 255)
            {
                if (Convert.ToInt32(P0, 16) != 254)
                {
                    if (eventPutDTO.EventCode is not null)
                    {
                        eventPutDTO.EventNameId = 14;
                    }
                    eventPutDTO.Clerk = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                    {
                            Convert.ToByte(Convert.ToInt32(P0,16))
                    });
                }
                else
                {
                    if (eventPutDTO.EventCode is not null)
                    {
                        eventPutDTO.EventNameId = 16;
                    }
                }
                string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
                eventPutDTO.ReasonSignout = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                {
                            Convert.ToByte(Convert.ToInt32(P3,16))
                });
            }
        }

        public static void _code_09h(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);

            if (eventPutDTO.EventCode is not null)
            {
                eventPutDTO.EventNameId = 17;
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.PrinterNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
               Convert.ToByte(Convert.ToInt32(P0,16))
            });
            eventPutDTO.PrinterPreviousStateId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
               Convert.ToByte(Convert.ToInt32(P1,16))
            });
            ++eventPutDTO.PrinterPreviousStateId;
            eventPutDTO.PrinterCurrentStateId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
               Convert.ToByte(Convert.ToInt32(P2,16))
            });
            ++eventPutDTO.PrinterCurrentStateId;
        }

        public static void _code_0ABh(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("0A"))
                {
                    eventPutDTO.EventNameId = 18;
                }
                else if (eventPutDTO.EventCode.Equals("0B"))
                {
                    eventPutDTO.EventNameId = 19;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P0,16)),
                            Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P2,16)),
            });
            eventPutDTO.Activity = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
            });
            eventPutDTO.WaitingTime = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
            });
        }

        public static void _code_10_11Ch(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            if (eventPutDTO.EventCode is not null)
            {
                if (eventPutDTO.EventCode.Equals("10"))
                {
                    eventPutDTO.EventNameId = 20;
                }
                else if (eventPutDTO.EventCode.Equals("11"))
                {
                    eventPutDTO.EventNameId = 21;
                }
            }
            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
        }

        public static void _code_0Ch(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                switch (P3)
                {
                    case "02":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 22;
                        }
                        break;
                    case "03":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 23;
                        }
                        break;
                    case "06":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 24;
                        }
                        break;
                    case "07":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 25;
                        }
                        break;
                    case "08":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 26;
                        }
                        break;
                    case "09":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 27;
                        }
                        break;
                    case "0B":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 28;
                        }
                        break;
                    case "0E":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 29;
                        }
                        break;
                    case "0F":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 30;
                        }
                        break;
                    case "14":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 31;
                        }
                        break;
                    case "15":
                        if (eventPutDTO.EventCode is not null)
                        {
                            eventPutDTO.EventNameId = 32;
                        }
                        break;
                }
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;
            int DiagnosticId = 0;

            switch (Convert.ToInt32(P3, 16))
            {
                case 2:
                case 3:
                case 6:
                case 11:
                    DiagnosticId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                    {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
                    });
                    DiagnosticId = DiagnosticFromCodeToDIagnosticID.FromCodeToDiagnosticID(DiagnosticId);
                    if (DiagnosticId != -1) { eventPutDTO.DiagnosticId = DiagnosticId; }
                    break;
                case 7:
                case 8:
                    DiagnosticId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                    {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
                    });
                    DiagnosticId = DiagnosticFromCodeToDIagnosticID.FromCodeToDiagnosticID(DiagnosticId);
                    if (DiagnosticId != -1) { eventPutDTO.DiagnosticId = DiagnosticId; }
                    eventPutDTO.Counter = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                     {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                     });
                    break;
                case 9:
                    DiagnosticId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                    {
                            Convert.ToByte(Convert.ToInt32(P3,16)),
                    });
                    DiagnosticId = DiagnosticFromCodeToDIagnosticID.FromCodeToDiagnosticID(DiagnosticId);
                    if (DiagnosticId != -1) { eventPutDTO.DiagnosticId = DiagnosticId; }
                    eventPutDTO.DiagnosticPin = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                     {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
                     }).ToString();
                    break;
                case 15:
                    eventPutDTO.ClientOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                    {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                    });
                    break;
                case 20:
                    eventPutDTO.DiagnosticPeriphTypeId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                     {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                     });
                    ++eventPutDTO.DiagnosticPeriphTypeId;
                    eventPutDTO.DiagnosticPeriphOrdinalNumber = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                     {
                            Convert.ToByte(Convert.ToInt32(P5,16)),
                     });
                    break;
                case 21:
                    eventPutDTO.DiagnosticData = Common.NumberFromBytes(eventAsByte.code, new Byte[]
                     {
                            Convert.ToByte(Convert.ToInt32(P4,16)),
                            Convert.ToByte(Convert.ToInt32(P5,16))
                     }).ToString();
                    break;
            }
        }

        public static void _code_0Dh(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                eventPutDTO.EventNameId = 31;
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;

            eventPutDTO.PacketNumOfBytes = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                    Convert.ToByte(Convert.ToInt32(P0,16))
            });
            eventPutDTO.PacketTypeId = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                    Convert.ToByte(Convert.ToInt32(P1,16))
            });
            eventPutDTO.PacketData = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                    Convert.ToByte(Convert.ToInt32(P2,16)),
                    Convert.ToByte(Convert.ToInt32(P3,16)),
                    Convert.ToByte(Convert.ToInt32(P4,16)),
                    Convert.ToByte(Convert.ToInt32(P5,16))
            }).ToString();
        }

        public static void _code_0Eh(ref EventAsByte eventAsByte, ref EventPutDTO eventPutDTO)
        {
            string P0 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 1);
            string P1 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 2);
            string P2 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 3);
            string P3 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 4);
            string P4 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 5);
            string P5 = Common.ReturnByteAsStringFromCode(eventAsByte.code, 6);

            if (eventPutDTO.EventCode is not null)
            {
                eventPutDTO.EventNameId = 32;
            }

            eventPutDTO.EventHex = eventAsByte.code;
            eventPutDTO.DateReceived = eventAsByte.date;

            eventPutDTO.PacketData = Common.NumberFromBytes(eventAsByte.code, new Byte[]
            {
                    Convert.ToByte(Convert.ToInt32(P0,16)),
                    Convert.ToByte(Convert.ToInt32(P1,16)),
                    Convert.ToByte(Convert.ToInt32(P2,16)),
                    Convert.ToByte(Convert.ToInt32(P3,16)),
                    Convert.ToByte(Convert.ToInt32(P4,16)),
                    Convert.ToByte(Convert.ToInt32(P5,16))
            }).ToString();
        }
    }
}
