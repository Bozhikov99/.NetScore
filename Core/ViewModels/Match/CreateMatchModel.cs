using Core.ViewModels.PlayerMatchStatistic;
using Core.ViewModels.TeamMatchStatistic;

namespace Core.ViewModels.Match
{
    public class CreateMatchModel
    {
        public IEnumerable<CreatePlayerMatchStatisticModel> PlayerMatchStatistics { get; set; }

        public CreateTeamMatchStatistic HomeTeamStatistics { get; set; }

        public CreateTeamMatchStatistic AwayTeamStatistics { get; set; }
    }
}
