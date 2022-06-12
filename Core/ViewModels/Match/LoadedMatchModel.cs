using Core.ViewModels.Player;
using Core.ViewModels.Team;

namespace Core.ViewModels.Match
{
    public class LoadedMatchModel
    {
        public string MatchCode { get; set; }

        public string TournamentId { get; set; }

        public IEnumerable<ListPlayerModel> HomePlayers { get; set; }

        public IEnumerable<ListPlayerModel> AwayPlayers { get; set; }

        public ListTeamModel HomeTeam { get; set; }

        public ListTeamModel AwayTeam { get; set; }
    }
}
