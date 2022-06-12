using AutoMapper;
using Core.ViewModels.Fixture;
using Infrastructure.Models;
using Infrastructure.Models.Enums;

namespace Core.Mapping
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            CreateMap<CreateFixtureModel, Fixture>();

            CreateMap<Fixture, ListFixtureModel>()
                .ForMember(d => d.IsEligible, s => s.MapFrom(f => 
                    f.AwayTeam.Players.Count >= 11 &&                 //I know this looks disgusting...
                    f.AwayTeam.Players.Where(p => p.Position == Position.GoalKeeper).Count() >= 1 &&
                    f.AwayTeam.Players.Where(p => p.Position == Position.Defender).Count() >= 3 &&
                    f.AwayTeam.Players.Where(p => p.Position == Position.Midfielder).Count() >= 2 &&
                    f.AwayTeam.Players.Where(p => p.Position == Position.Striker).Count() >= 1 &&
                    f.HomeTeam.Players.Count >= 11 &&
                    f.HomeTeam.Players.Where(p => p.Position == Position.GoalKeeper).Count() >= 1 &&
                    f.HomeTeam.Players.Where(p => p.Position == Position.Defender).Count() >= 3 &&
                    f.HomeTeam.Players.Where(p => p.Position == Position.Midfielder).Count() >= 2 &&
                    f.HomeTeam.Players.Where(p => p.Position == Position.Striker).Count() >= 1));
        }
    }
}
