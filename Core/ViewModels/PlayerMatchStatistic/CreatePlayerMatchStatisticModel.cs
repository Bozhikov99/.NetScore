using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.PlayerMatchStatistic
{
    public class CreatePlayerMatchStatisticModel
    {
        public int Goals { get; set; }

        public int Assists { get; set; }

        public int Passes { get; set; }

        public int Tackles { get; set; }

        public int Saves { get; set; }

        public int Fouls { get; set; }

        [Required]
        public string MatchCode { get; set; }

        [Required]
        public string PlayerId { get; set; }

        [Required]
        public string TeamId { get; set; }
    }
}
