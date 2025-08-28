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
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleGetDTO>()
                .ForMember(dest => dest.AppIds, opt => opt.MapFrom(src => src.Apps.Select(x => x.AppId)))
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.Users.Select(x => x.UserId)));

            CreateMap<RoleGetDTO, Role>();

            CreateMap<Role, RolePutDTO>()
                .ForMember(dest => dest.AppPutDTOs, opt => opt.MapFrom(src => src.Apps))
                .ForMember(dest => dest.UserPutDTOs, opt => opt.MapFrom(src => src.Users));

            CreateMap<RolePutDTO, Role>()
                .ForMember(dest => dest.Apps, opt => opt.MapFrom(src => src.AppPutDTOs))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserPutDTOs));
        }
    }
}
