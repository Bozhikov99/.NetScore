using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PlayerMatchStatistic
    {
        public PlayerMatchStatistic()
        {
            Id = Guid.NewGuid()
                .ToString();
        }

        [Key]
        public string Id { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int Passes { get; set; }

        public int Tackles { get; set; }

        public int Saves { get; set; }

        public int Fouls { get; set; }

        [Required]
        public string MatchCode { get; set; }

        [ForeignKey(nameof(Player))]
        public string PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}
