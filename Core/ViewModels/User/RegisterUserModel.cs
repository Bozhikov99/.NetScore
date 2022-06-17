using Common.ErrorMessageConstants;
using Common.ValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.User
{
    public class RegisterUserModel
    {
        [Required]
        [StringLength(UserConstants.USERNAME_MAXLENGTH, MinimumLength = UserConstants.USERNAME_MINLENGTH, ErrorMessage = CommonErrorConstants.INTERVAL_ERROR)]
        public string UserName { get; set; }

        [Required]
        [StringLength(UserConstants.PASSWORD_MAXLENGTH, MinimumLength = UserConstants.USERNAME_MINLENGTH, ErrorMessage = CommonErrorConstants.INTERVAL_ERROR)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = UserErrorConstants.PASSWORDS_MATCH)]
        public string ConfirmPassword { get; set; }
    }
}
