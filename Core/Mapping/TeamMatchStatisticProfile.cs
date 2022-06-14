using AutoMapper;
using Core.ViewModels.TeamMatchStatistic;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class TeamMatchStatisticProfile : Profile
    {
        public TeamMatchStatisticProfile()
        {
            CreateMap<CreateTeamMatchStatistic, TeamMatchStatistic>();
        }
    }
}
