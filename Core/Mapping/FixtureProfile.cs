using AutoMapper;
using Core.ViewModels.Fixture;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            CreateMap<CreateFixtureModel, Fixture>();

            CreateMap<Fixture, ListFixtureModel>();
        }
    }
}
