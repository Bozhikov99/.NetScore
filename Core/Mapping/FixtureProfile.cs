using AutoMapper;
using Core.ViewModels.Fixture;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            CreateMap<CreateFixtureModel, Fixture>();
        }
    }
}
