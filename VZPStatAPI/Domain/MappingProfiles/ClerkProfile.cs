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
    public class ClerkProfile : Profile
    {
        public ClerkProfile()
        {
            CreateMap<Clerk, ClerkGetDTO>()
                .ForMember(dest => dest.CounterGetDTOs,
                    opt => opt.MapFrom(src => src.Counters))
                .ForMember(dest => dest.ClientGetDTOs,
                    opt => opt.MapFrom(src => src.Clients));

            CreateMap<ClerkGetDTO, Clerk>()
                .ForMember(dest => dest.Counters,
                    opt => opt.MapFrom(src => src.CounterGetDTOs))
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientGetDTOs));

            CreateMap<Clerk, ClerkPutDTO>()
                .ForMember(dest => dest.CounterPutDTOs,
                    opt => opt.MapFrom(src => src.Counters))
                .ForMember(dest => dest.ClientPutDTOs,
                    opt => opt.MapFrom(src => src.Clients));

            CreateMap<ClerkPutDTO, Clerk>()
                .ForMember(dest => dest.Counters,
                    opt => opt.MapFrom(src => src.CounterPutDTOs))
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientPutDTOs));
        }
    }
}
