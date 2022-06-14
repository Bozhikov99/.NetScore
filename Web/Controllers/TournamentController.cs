using Core.Services;
using Core.Services.Contracts;
using Core.ViewModels.Fixture;
using Core.ViewModels.Match;
using Core.ViewModels.Player;
using Core.ViewModels.Team;
using Core.ViewModels.Tournament;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;
        private readonly IMatchService matchService;

        public TournamentController(
            ITournamentService tournamentService,
            ITeamService teamService,
            IPlayerService playerService,
            IMatchService matchService)
        {
            this.tournamentService = tournamentService;
            this.teamService = teamService;
            this.playerService = playerService;
            this.matchService = matchService;
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
            bool isScheduled = await tournamentService.IsScheduled(id);

            if (isScheduled)
            {
                return RedirectToAction("Error");
            }

            IEnumerable<ListTeamModel> teams = await teamService.GetTeamsForTournament(id);
            ViewBag.Teams = teams;
            ViewBag.TournamentId = id;

            return View();
        }


        public async Task<IActionResult> Details(string id)
        {
            TournamentDetailsModel details = await tournamentService.GetDetails(id);
            IEnumerable<ListTeamModel> teams = await teamService.GetTeamsForTournament(id);
            IEnumerable<ListFixtureModel> fixtures = await tournamentService.GetFixtures(id);

            ViewBag.Teams = teams;
            ViewBag.Fixtures = fixtures;

            return View(details);
        }

        public async Task<IActionResult> LineUp(string homeId, string awayId, string id)
        {
            IEnumerable<ListPlayerModel> homePlayers = await playerService.GetAll(homeId);
            IEnumerable<ListPlayerModel> awayPlayers = await playerService.GetAll(awayId);
            ViewBag.HomePlayers = homePlayers;
            ViewBag.AwayPlayers = awayPlayers;
            ViewBag.TournamentId = id;

            return View();
        }

        //public async Task<IActionResult> Match(string homeId, string awayId)
        //{


        //    return View();
        //}

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

            if (!ModelState.IsValid)
            {
                throw new ArgumentNullException("Empty fixture");
            }
            await tournamentService.Schedule(schedule);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoadMatch(string[] homeIds, string[] awayIds, string tournamentId)
        {
            LoadedMatchModel match = await matchService.LoadMatch(homeIds, awayIds, tournamentId);

            return View(match);
        }

        [HttpPost]
        public async Task<IActionResult> Play(CreateMatchModel model)
        {
            try
            {
                await matchService.CreateMatch(model);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
    }
}
