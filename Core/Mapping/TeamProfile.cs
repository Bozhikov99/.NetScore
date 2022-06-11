using AutoMapper;
using Core.ViewModels.Team;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, ListTeamModel>();

            CreateMap<CreateTeamModel, Team>();

            CreateMap<EditTeamModel, Team>()
                .ReverseMap();

            CreateMap<Team, TeamDetailsModel>()
                .ForMember(d => d.Fans, s => s.MapFrom(t => t.Fans.Count))
                .ForMember(d => d.Wins, s => s.MapFrom(t => t.TeamMatchStatistics.Where(m => m.IsWinner == true).Count()))
                .ForMember(d => d.Losses, s => s.MapFrom(t => t.TeamMatchStatistics.Where(m => m.IsWinner == false).Count()));
        }
    }
}
