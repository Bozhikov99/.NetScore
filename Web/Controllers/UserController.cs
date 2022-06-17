using Common.ErrorMessageConstants;
using Core.Services.Contracts;
using Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login() => View();

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                IdentityResult isRegistered = await userService.Register(model);

                if (!isRegistered.Succeeded)
                {
                    return View();
                }
            }
            catch (ArgumentException ae)
            {
                return View("Error", ae.Message);
            }
            catch (Exception)
            {
                return View("Error", UserErrorConstants.REGISTER_UNEXPECTED);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await userService.Login(model);
            }
            catch (ArgumentException ae)
            {
                return View("Error", ae.Message);
            }
            catch (Exception)
            {
                return View("Error", UserErrorConstants.LOGIN_UNEXPECTED);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
