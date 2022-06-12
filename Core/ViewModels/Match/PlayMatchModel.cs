using Core.ViewModels.Player;
using Core.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Match
{
    public class PlayMatchModel
    {
        public IEnumerable<ListPlayerModel> HomePlayers { get; set; }

        public IEnumerable<ListPlayerModel> AwayPlayers { get; set; }

        public ListTeamModel HomeTeam { get; set; }

        public ListTeamModel AwayTeam { get; set; }
    }
}
