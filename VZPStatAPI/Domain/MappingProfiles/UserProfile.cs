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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDTO>()
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.Roles.Select(x => x.RoleId)))
                .ForMember(dest => dest.VZPCodes, opt => opt.MapFrom(src => src.Branches.Select(x => x.VZP_code)))
                .ForMember(dest => dest.BranchIds, opt => opt.MapFrom(src => src.Branches.Select(x => x.BranchId)));

            CreateMap<UserGetDTO, User>();

            CreateMap<User, UserPutDTO>()
                .ForMember(dest => dest.RolePutDTOs, opt => opt.MapFrom(src => src.Roles))
                .ForMember(dest => dest.BranchPutDTOs, opt => opt.MapFrom(src => src.Branches));

            CreateMap<UserPutDTO, User>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RolePutDTOs))
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.BranchPutDTOs));
        }
    }
}
