using Common.ErrorMessageConstants;
using Common.ValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Player
{
    public class EditPlayerModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(PlayerConstants.NAME_MAXLENGTH, MinimumLength = PlayerConstants.NAME_MINLENGTH, ErrorMessage = CommonErrorConstants.INTERVAL_ERROR)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(PlayerConstants.NAME_MAXLENGTH, MinimumLength = PlayerConstants.NAME_MINLENGTH, ErrorMessage = CommonErrorConstants.INTERVAL_ERROR)]
        public string LastName { get; set; }

        [Range(PlayerConstants.HEIGHT_MIN, PlayerConstants.HEIGHT_MAX, ErrorMessage = CommonErrorConstants.INTERVAL_ERROR)]
        public int Height { get; set; }

        public string PreferredFoot { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public string TeamId { get; set; }
    }
}
