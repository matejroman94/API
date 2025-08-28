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
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<App, AppGetDTO>()
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.Roles.Select(x => x.RoleId)));

            CreateMap<AppGetDTO, App>();

            CreateMap<App, AppPutDTO>()
                .ForMember(dest => dest.RolePutDTOs, opt => opt.MapFrom(src => src.Roles));

            CreateMap<AppPutDTO, App>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RolePutDTOs));
        }
    }
}
