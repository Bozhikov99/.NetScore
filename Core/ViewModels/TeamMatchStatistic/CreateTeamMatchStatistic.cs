using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.TeamMatchStatistic
{
    public class CreateTeamMatchStatistic
    {
        public bool IsWinner { get; set; }

        public int Goals { get; set; }

        [Required]
        public string MatchCode { get; set; }

        [Required]
        public string TournamentId { get; set; }

        [Required]
        public string TeamId { get; set; }
    }
}
