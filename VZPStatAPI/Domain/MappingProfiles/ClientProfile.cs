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
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientGetDTO>()
                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src => (src.Branch != null) ? src.Branch.BranchName : string.Empty))
                .ForMember(dest => dest.ClerkName,
                    opt => opt.MapFrom(src => (src.Clerk != null) ? src.Clerk.ClerkName : string.Empty))
                .ForMember(dest => dest.ActivityName,
                    opt => opt.MapFrom(src => (src.Activity != null) ? src.Activity.ActivityName : string.Empty))
                .ForMember(dest => dest.PrinterName,
                    opt => opt.MapFrom(src => (src.Printer != null) ? src.Printer.PrinterName : string.Empty))
                .ForMember(dest => dest.CounterName,
                    opt => opt.MapFrom(src => (src.Counter != null) ? src.Counter.CounterName : string.Empty))
                .ForMember(dest => dest.Client_Status,
                    opt => opt.MapFrom(src => (src.ClientStatus != null) ? src.ClientStatus.Status : string.Empty))
                .ForMember(dest => dest.ClientDone_Description,
                    opt => opt.MapFrom(src => (src.ClientDone != null) ? src.ClientDone.Description : string.Empty))
                .ForMember(dest => dest.TransferReason_Name,
                    opt => opt.MapFrom(src => (src.TransferReason != null) ? src.TransferReason.Description : string.Empty));

            CreateMap<ClientGetDTO, Client>();

            CreateMap<Client, ClientPutDTO>().ReverseMap();
        }
    }
}
