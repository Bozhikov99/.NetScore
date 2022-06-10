using Common.ValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Tournament
    {
        public Tournament()
        {
            Id = Guid.NewGuid()
                .ToString();

            Teams = new List<Team>();

            TeamMatchStatistics = new List<TeamMatchStatistic>();
            Fixtures = new List<Fixture>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(TournamentConstants.NAME_MAXLENGTH)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<TeamMatchStatistic> TeamMatchStatistics { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }
    }
}
