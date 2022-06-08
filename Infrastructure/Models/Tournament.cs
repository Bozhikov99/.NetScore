using System;
using System.Collections.Generic;
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
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<TeamMatchStatistic> TeamMatchStatistics { get; set; }
    }
}
