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
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<Branch, BranchGetDTO>()
                .ForMember(dest => dest.RegionName,
                    opt => opt.MapFrom(src => (src.Region != null) ? src.Region.RegionName : string.Empty))
                .ForMember(dest => dest.ParentBranchName,
                    opt => opt.MapFrom(src => (src.ParentBranch != null) ? src.ParentBranch.BranchName : string.Empty))
                .ForMember(dest => dest.ActivityGetDTOs,
                    opt => opt.MapFrom(src => src.Activities))
                .ForMember(dest => dest.PrinterGetDTOs,
                    opt => opt.MapFrom(src => src.Printers))
                .ForMember(dest => dest.EventGetDTOs,
                    opt => opt.MapFrom(src => src.Events))
                .ForMember(dest => dest.CounterGetDTOs,
                    opt => opt.MapFrom(src => src.Counters))
                .ForMember(dest => dest.ClientGetDTOs,
                    opt => opt.MapFrom(src => src.Clients))
                .ForMember(dest => dest.DiagnosticBranchGetDTOs,
                    opt => opt.MapFrom(src => src.DiagnosticBranches));

            CreateMap<BranchGetDTO, Branch>()
                .ForMember(dest => dest.Activities,
                    opt => opt.MapFrom(src => src.ActivityGetDTOs))
                .ForMember(dest => dest.Printers,
                    opt => opt.MapFrom(src => src.PrinterGetDTOs))
                .ForMember(dest => dest.Events,
                    opt => opt.MapFrom(src => src.EventGetDTOs))
                .ForMember(dest => dest.Counters,
                    opt => opt.MapFrom(src => src.CounterGetDTOs))
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientGetDTOs))
                .ForMember(dest => dest.DiagnosticBranches,
                    opt => opt.MapFrom(src => src.DiagnosticBranchGetDTOs));

            CreateMap<Branch, BranchPutDTO>()
                .ForMember(dest => dest.ActivityPutDTOs,
                    opt => opt.MapFrom(src => src.Activities))
                .ForMember(dest => dest.PrinterPutDTOs,
                    opt => opt.MapFrom(src => src.Printers))
                .ForMember(dest => dest.EventPutDTOs,
                    opt => opt.MapFrom(src => src.Events))
                .ForMember(dest => dest.CounterPutDTOs,
                    opt => opt.MapFrom(src => src.Counters))
                .ForMember(dest => dest.ClientPutDTOs,
                    opt => opt.MapFrom(src => src.Clients))
                .ForMember(dest => dest.DiagnosticBranchPutDTOs,
                    opt => opt.MapFrom(src => src.DiagnosticBranches));

            CreateMap<BranchPutDTO, Branch>()
                .ForMember(dest => dest.Activities,
                    opt => opt.MapFrom(src => src.ActivityPutDTOs))
                .ForMember(dest => dest.Printers,
                    opt => opt.MapFrom(src => src.PrinterPutDTOs))
                .ForMember(dest => dest.Events,
                    opt => opt.MapFrom(src => src.EventPutDTOs))
                .ForMember(dest => dest.Counters,
                    opt => opt.MapFrom(src => src.CounterPutDTOs))
                .ForMember(dest => dest.Clients,
                    opt => opt.MapFrom(src => src.ClientPutDTOs))
                .ForMember(dest => dest.DiagnosticBranches,
                    opt => opt.MapFrom(src => src.DiagnosticBranchPutDTOs));
        }
    }
}
