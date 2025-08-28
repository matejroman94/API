using AutoMapper;
using Domain.DataDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MappingProfiles
{
    public class LoggerProfile : Profile
    {
        public LoggerProfile()
        {
            CreateMap<Domain.Models.Logger, LoggerGetDTO>().ReverseMap();
            CreateMap<Domain.Models.Logger, LoggerPutDTO>().ReverseMap();
        }
    }
}
