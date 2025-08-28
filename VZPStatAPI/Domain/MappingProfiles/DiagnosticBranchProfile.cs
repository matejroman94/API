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
    public class DiagnosticBranchProfile : Profile
    {
        public DiagnosticBranchProfile()
        {
            CreateMap<DiagnosticBranch, DiagnosticBranchGetDTO>().ReverseMap();
            CreateMap<DiagnosticBranch, DiagnosticBranchPutDTO>().ReverseMap();
        }
    }
}
