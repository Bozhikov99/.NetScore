using System;
using System.Collections.Generic;
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
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Country { get; set; }

        public int Trophies { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }

        public virtual ICollection<TeamMatchStatistic> TeamMatchStatistics { get; set; }

        public virtual ICollection<User> Fans { get; set; }
    }
}
