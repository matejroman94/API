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
    public class PrinterProfile : Profile
    {
        public PrinterProfile()
        {
            CreateMap<Printer, PrinterGetDTO>()
                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src => (src.Branch != null) ? src.Branch.IpAddress : string.Empty))
                .ForMember(dest => dest.PrinterCurrentStatus,
                    opt => opt.MapFrom(src => (src.PrinterCurrentStatus != null) ? src.PrinterCurrentStatus.Description : string.Empty))
                .ForMember(dest => dest.PrinterPreviousStatus,
                    opt => opt.MapFrom(src => (src.PrinterPreviousStatus != null) ? src.PrinterPreviousStatus.Description : string.Empty))
                .ForMember(dest => dest.ClientGetDTOs,
                    opt => opt.MapFrom(src => src.Clients));

            CreateMap<PrinterGetDTO, Printer>()
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientGetDTOs));

            CreateMap<Printer, PrinterPutDTO>()
                .ForMember(dest => dest.ClientPutDTOs,
                    opt => opt.MapFrom(src => src.Clients));

            CreateMap<PrinterPutDTO, Printer>()
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientPutDTOs));
        }
    }
}
