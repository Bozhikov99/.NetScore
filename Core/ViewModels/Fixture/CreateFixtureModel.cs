using Common.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Fixture
{
    public class CreateFixtureModel
    {
        [Required]
        [MinLength(FixtureConstants.ID_MINLENGTH)]
        public string HomeTeamId { get; set; }

        [Required]
        [MinLength(FixtureConstants.ID_MINLENGTH)]
        public string AwayTeamId { get; set; }

        [Required]
        [MinLength(FixtureConstants.ID_MINLENGTH)]
        public string TournamentId { get; set; }
    }
}
