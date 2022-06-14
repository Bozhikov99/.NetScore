using AutoMapper;
using Core.ViewModels.PlayerMatchStatistic;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class PlayerMatchStatisticProfile : Profile
    {
        public PlayerMatchStatisticProfile()
        {
            CreateMap<CreatePlayerMatchStatisticModel, PlayerMatchStatistic>();
        }
    }
}
