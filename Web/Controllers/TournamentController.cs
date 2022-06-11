using Core.Services.Contracts;
using Core.ViewModels.Fixture;
using Core.ViewModels.Team;
using Core.ViewModels.Tournament;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;

        public TournamentController(
            ITournamentService tournamentService,
            ITeamService teamService)
        {
            this.tournamentService = tournamentService;
            this.teamService = teamService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<ListTournamentModel> tournaments = await tournamentService.GetAll();

            return View(tournaments);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<ListTeamModel> teams = await teamService.GetCompleteTeams();
            ViewBag.Teams = teams;

            return View();
        }

        public async Task<IActionResult> Schedule(string id)
        {
            IEnumerable<ListTeamModel> teams = await teamService.GetTeamsForTournament(id);
            ViewBag.Teams = teams;
            ViewBag.TournamentId = id;

            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            TournamentDetailsModel details = await tournamentService.GetDetails(id);
            IEnumerable<ListTeamModel> teams = await teamService.GetTeamsForTournament(id);

            ViewBag.Teams = teams;
            ViewBag.Fixtures = teams.Count() / 2;
            ViewBag.TournamentId = id;

            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentModel model)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ListTeamModel> teams = await teamService.GetCompleteTeams();
                ViewBag.Teams = teams;

                return View();
            }
            await tournamentService.Create(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(CreateFixtureModel[] schedule)
        {
            string tournamentId = schedule.First().TournamentId;

            if (!ModelState.IsValid)
            {
                //IEnumerable<ListTeamModel> teams = await teamService.GetTeamsForTournament(tournamentId);
                //ViewBag.Teams = teams;
                //ViewBag.TournamentId = tournamentId;

                //return View();
                throw new ArgumentNullException("Empty fixture");
            }
            await tournamentService.Schedule(schedule);

            return Ok();
        }
    }
}
