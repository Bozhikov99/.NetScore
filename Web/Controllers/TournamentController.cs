﻿using Core.Services.Contracts;
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
            ViewBag.Teams=teams;

            return View();
        }

        public async Task<IActionResult> Schedule(CreateFixtureModel[] schedule)
        {
            string tournamentId = schedule.First().TournamentId;
            await tournamentService.Schedule(schedule);

            return RedirectToAction(nameof(Details), new { tournamentId });
        }

        public async Task<IActionResult> Details(string id)
        {

            return Ok();
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
    }
}