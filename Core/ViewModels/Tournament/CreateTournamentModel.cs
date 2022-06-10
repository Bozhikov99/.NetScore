using Common.ValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Tournament
{
    public class CreateTournamentModel
    {
        [Required]
        [MaxLength(TournamentConstants.NAME_MAXLENGTH)]
        public string Name { get; set; }

        [MinLength(TournamentConstants.TEAMS_MINLENGTH)]
        [MaxLength(TournamentConstants.TEAMS_MAXLENGTH)]
        public string[] TeamIds { get; set; }
    }
}
