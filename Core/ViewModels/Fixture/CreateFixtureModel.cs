using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Fixture
{
    public class CreateFixtureModel
    {
        [Required]
        public string HomeTeamId { get; set; }

        [Required]
        public string AwayTeamId { get; set; }

        [Required]
        public string TournamentId { get; set; }
    }
}
