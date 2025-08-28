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
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityGetDTO>()
                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src => (src.Branch != null) ? src.Branch.BranchName : string.Empty));
            CreateMap<ActivityGetDTO, Activity>();
            CreateMap<Activity, ActivityPutDTO>().ReverseMap();
        }
    }
}
