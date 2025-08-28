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
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionGetDTO>()
                .ForMember(dest => dest.BranchesGetDTOs,
                    opt => opt.MapFrom(src => src.Branches));

            CreateMap<RegionGetDTO, Region>()
                .ForMember(dest => dest.Branches,
                    opt => opt.MapFrom(src => src.BranchesGetDTOs));

            CreateMap<Region, RegionPutDTO>()
                .ForMember(dest => dest.BranchesPutDTOs,
                    opt => opt.MapFrom(src => src.Branches));

            CreateMap<RegionPutDTO, Region>()
                .ForMember(dest => dest.Branches,
                    opt => opt.MapFrom(src => src.BranchesPutDTOs));
        }
    }
}
