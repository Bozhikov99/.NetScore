using AutoMapper;
using Core.ViewModels.Tournament;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<CreateTournamentModel, Tournament>();

            CreateMap<Tournament, ListTournamentModel>();

            CreateMap<Tournament, TournamentDetailsModel>();
        }
    }
}
