using Core.Services.Contracts;
using Core.ViewModels.Player;
using Core.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;

        public TeamController(ITeamService teamService, IPlayerService playerService)
        {
            this.teamService = teamService;
            this.playerService = playerService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<ListTeamModel> teams = await teamService.GetAll();

            return View(teams);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<ListPlayerModel> players = await playerService.GetAll();
            ViewBag.Players = players;

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            EditTeamModel team = await teamService.GetEditModel(id);
            IEnumerable<ListPlayerModel> players = await playerService.GetAll();
            ViewBag.Players = players;

            return View(team);
        }

        public async Task<IActionResult> Details(string id)
        {
            TeamDetailsModel details = await teamService.Details(id);

            return View(details);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await teamService.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamModel model)
        {
            try
            {
                await teamService.Edit(model);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamModel model)
        {
            try
            {
                await teamService.Create(model);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(All));
        }
    }
}
