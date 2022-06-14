using Core.ViewModels.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IMatchService
    {
        Task<LoadedMatchModel> LoadMatch(string[] homeIds, string[] awayIds, string tournamentId);

        Task CreateMatch(CreateMatchModel model);
    }
}
