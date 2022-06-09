using Common.ErrorMessageConstants;
using Common.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Team
{
    public class CreateTeamModel
    {
        [Required]
        [StringLength(TeamConstants.NAME_MAXLENGTH, MinimumLength = TeamConstants.NAME_MINLENGTH, ErrorMessage = CommonErrorConstants.INTERVAL_ERROR)]
        public string Name { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Color { get; set; }

        public string[]? PlayerIds { get; set; }
    }
}
