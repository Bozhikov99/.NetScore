using Common.ValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Team
    {
        public Team()
        {
            Id = Guid.NewGuid()
                .ToString();

            Players = new List<Player>();
            Tournaments = new List<Tournament>();
            TeamMatchStatistics = new List<TeamMatchStatistic>();
            Fans = new List<User>();
            HomeFixtures = new List<Fixture>();
            AwayFixtures = new List<Fixture>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(TeamConstants.NAME_MAXLENGTH)]
        public string Name { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        public string Country { get; set; }

        public int Trophies { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }

        public virtual ICollection<TeamMatchStatistic> TeamMatchStatistics { get; set; }

        [InverseProperty("AwayTeam")]
        public virtual ICollection<Fixture> AwayFixtures { get; set; }

        [InverseProperty("HomeTeam")]
        public virtual ICollection<Fixture> HomeFixtures { get; set; }

        public virtual ICollection<User> Fans { get; set; }
    }
}
