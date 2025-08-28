using AutoMapper;
using Domain.DataDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.MappingProfiles
{
    public class CounterProfile : Profile
    {
        public CounterProfile()
        {
            CreateMap<Counter, CounterGetDTO>()
                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src => (src.Branch != null) ? src.Branch.BranchName : string.Empty))
                .ForMember(dest => dest.CounterStatus,
                    opt => opt.MapFrom(src => (src.CounterStatus != null) ? src.CounterStatus.Status : string.Empty))
                .ForMember(dest => dest.ClerkGetDTOs,
                    opt => opt.MapFrom(src => src.Clerks))
                .ForMember(dest => dest.ClientGetDTOs,
                    opt => opt.MapFrom(src => src.Clients));

            CreateMap<CounterGetDTO, Counter>()
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientGetDTOs))
                .ForMember(dest => dest.Clerks,
                    opt => opt.MapFrom(src => src.ClerkGetDTOs));

            CreateMap<Counter, CounterPutDTO>()
                .ForMember(dest => dest.ClerkPutDTOs,
                    opt => opt.MapFrom(src => src.Clerks))
                .ForMember(dest => dest.ClientPutDTOs,
                    opt => opt.MapFrom(src => src.Clients));

            CreateMap<CounterPutDTO, Counter>()
                .ForMember(dest => dest.Clerks,
                    opt => opt.MapFrom(src => src.ClerkPutDTOs))
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientPutDTOs));
        }
    }
}
