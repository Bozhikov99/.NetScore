using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Fixture
    {
        public Fixture()
        {
            Id = Guid.NewGuid()
                .ToString();

            MatchCode = Guid.NewGuid()
                .ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string MatchCode { get; set; }

        [Required]
        [ForeignKey(nameof(HomeTeam))]
        public string HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        [Required]
        [ForeignKey(nameof(AwayTeam))]
        public string AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        [Required]
        [ForeignKey(nameof(TournamentId))]
        public string TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
