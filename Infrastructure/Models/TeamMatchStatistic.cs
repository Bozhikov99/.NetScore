using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class TeamMatchStatistic
    {
        public TeamMatchStatistic()
        {
            Id = Guid.NewGuid()
                .ToString();

            PlayerMatchStatistics = new List<PlayerMatchStatistic>();
        }

        public string Id { get; set; }

        public bool IsWinner { get; set; }

        [Required]
        public string MatchCode { get; set; }

        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }

        public virtual Team Team { get; set; }

        [ForeignKey(nameof(Tournament))]
        public string TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<PlayerMatchStatistic> PlayerMatchStatistics { get; set; }
    }
}
