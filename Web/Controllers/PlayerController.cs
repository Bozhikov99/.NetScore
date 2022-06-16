using Common.ErrorMessageConstants;
using Common.ValidationConstants;
using Core.Services.Contracts;
using Core.ViewModels.Player;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<ListPlayerModel> players = await playerService.GetAll();

            return View(players);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = PlayerConstants.POSITIONS;
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            EditPlayerModel editPlayerModel = await playerService.GetEditModel(id);
            ViewBag.Positions = PlayerConstants.POSITIONS;

            return View(editPlayerModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                PlayerDetailsModel details = await playerService.Details(id);

                return View(details);
            }
            catch (Exception)
            {
                return View("Error", CommonErrorConstants.UNEXTECTED_ERROR);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await playerService.Create(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return View("Error", PlayerErrorConstants.UNEXPECTED_CREATING);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlayerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await playerService.Edit(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return View("Error", PlayerErrorConstants.UNEXPECTED_EDITING);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await playerService.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }

            return Ok();
        }
    }
}
