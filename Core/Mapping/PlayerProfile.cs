using AutoMapper;
using Core.ViewModels.Player;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<CreatePlayerModel, Player>();

            CreateMap<EditPlayerModel, Player>()
                .ReverseMap();

            CreateMap<Player, PlayerDetailsModel>()
                .ForMember(d => d.Goals, s => s.MapFrom(p => p.Statistics.Select(stat => stat.Goals).Sum()))
                .ForMember(d => d.Assists, s => s.MapFrom(p => p.Statistics.Select(stat => stat.Assists).Sum()))
                .ForMember(d => d.Passes, s => s.MapFrom(p => p.Statistics.Select(stat => stat.Passes).Sum()))
                .ForMember(d => d.Saves, s => s.MapFrom(p => p.Statistics.Select(stat => stat.Saves).Sum()))
                .ForMember(d => d.Fouls, s => s.MapFrom(p => p.Statistics.Select(stat => stat.Fouls).Sum()))
                .ForMember(d => d.Tackles, s => s.MapFrom(p => p.Statistics.Select(stat => stat.Tackles).Sum()))
                .ForMember(d => d.Fans, s => s.MapFrom(p => p.Fans.Count()));

            CreateMap<Player, ListPlayerModel>()
                .ForMember(d => d.Team, s => s.MapFrom(p => p.Team.Name))
                .ForMember(d => d.TeamImageUrl, s => s.MapFrom(p => p.Team.LogoUrl));
        }
    }
}
