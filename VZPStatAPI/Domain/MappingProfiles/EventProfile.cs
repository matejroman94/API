using AutoMapper;
using Domain.DataDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MappingProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventGetDTO>()
                .ForMember(dest => dest.Branch,
                    opt => opt.MapFrom(src => (src.Branch != null) ? src.Branch.IpAddress : string.Empty))
                .ForMember(dest => dest.EventName,
                    opt => opt.MapFrom(src => (src.EventName != null) ? src.EventName.Name : string.Empty))
                .ForMember(dest => dest.Diagnostic,
                    opt => opt.MapFrom(src => (src.Diagnostic != null) ? src.Diagnostic.Description : string.Empty))
                .ForMember(dest => dest.PrinterCurrentStatus,
                    opt => opt.MapFrom(src => (src.PrinterCurrentStatus != null) ? src.PrinterCurrentStatus.Description : string.Empty))
                .ForMember(dest => dest.PrinterPreviousStatus,
                    opt => opt.MapFrom(src => (src.PrinterPreviousStatus != null) ? src.PrinterPreviousStatus.Description : string.Empty))
                .ForMember(dest => dest.DiagnosticPeriphType,
                    opt => opt.MapFrom(src => (src.DiagnosticPeriphType != null) ? src.DiagnosticPeriphType.Description : string.Empty))
                .ForMember(dest => dest.PacketType,
                    opt => opt.MapFrom(src => (src.PacketType != null) ? src.PacketType.Description : string.Empty))
                .ForMember(dest => dest.ReasonName,
                    opt => opt.MapFrom(src => (src.Reason != null) ? src.Reason.Description : string.Empty))
                .ForMember(dest => dest.TransferReason_Name,
                    opt => opt.MapFrom(src => (src.TransferReason != null) ? src.TransferReason.Description : string.Empty));

            CreateMap<EventGetDTO, Event>();

            CreateMap<Event, EventPutDTO>().ReverseMap();
        }
    }
}
