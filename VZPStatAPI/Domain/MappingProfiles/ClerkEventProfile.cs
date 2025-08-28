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
    public class ClerkEventProfile : Profile
    {
        public ClerkEventProfile()
        {
            CreateMap<ClerkEvent, ClerkEventGetDTO>()
                .ForMember(dest => dest.ClerkName,
                    opt => opt.MapFrom(src => (src.Clerk != null) ? src.Clerk.ClerkName : string.Empty))
                .ForMember(dest => dest.ClerkStatus_Status,
                    opt => opt.MapFrom(src => (src.ClerkStatus != null) ? src.ClerkStatus.Status : string.Empty));

            CreateMap<ClerkEventGetDTO, ClerkEvent>();

            CreateMap<ClerkEvent, ClerkEventPutDTO>().ReverseMap();
        }
    }
}
