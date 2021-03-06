using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Services.Contracts;
using Core.ViewModels.Fixture;
using Core.ViewModels.Team;
using Core.ViewModels.Tournament;
using Infrastructure.Common;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public TournamentService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ListTournamentModel>> GetAll()
        {
            IEnumerable<ListTournamentModel> tournaments = await repository.All<Tournament>()
                .ProjectTo<ListTournamentModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return tournaments;
        }

        public async Task Create(CreateTournamentModel model)
        {
            Tournament tournament = mapper.Map<Tournament>(model);

            foreach (string teamId in model.TeamIds)
            {
                Team currentTeam = await repository.GetByIdAsync<Team>(teamId);
                tournament.Teams.Add(currentTeam);
            }

            tournament.IsActive = true;

            await repository.AddAsync(tournament);
            await repository.SaveChangesAsync();
        }

        public async Task Schedule(CreateFixtureModel[] schedule)
        {
            string tournamentId = schedule.First().TournamentId;
            Tournament tournament = await repository.GetByIdAsync<Tournament>(tournamentId);
            bool isAlreadyScheduled = await IsScheduled(tournamentId);

            if (!tournament.IsActive || isAlreadyScheduled)
            {
                throw new InvalidOperationException("Tournament is over or already scheduled");
            }


            List<Fixture> fixtures = new List<Fixture>();

            foreach (CreateFixtureModel f in schedule)
            {
                Fixture fixture = mapper.Map<Fixture>(f);
                fixtures.Add(fixture);
            }

            await repository.AddRangeAsync(fixtures);
            await repository.SaveChangesAsync();
        }

        public async Task<TournamentDetailsModel> GetDetails(string id)
        {
            var tournament = await repository.All<Tournament>()
                .Include(t => t.Fixtures)
                .FirstAsync(t => t.Id == id);

            TournamentDetailsModel details = mapper.Map<TournamentDetailsModel>(tournament);

            return details;
        }

        public async Task<IEnumerable<ListFixtureModel>> GetFixtures(string id)
        {
            IEnumerable<ListFixtureModel> fixtures = await repository.All<Fixture>(f => f.TournamentId == id)
                .ProjectTo<ListFixtureModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return fixtures;
        }

        public async Task<bool> IsScheduled(string id)
        {
            Tournament tournament = await repository.All<Tournament>()
                .Include(t => t.TeamMatchStatistics)
                .Include(t => t.Fixtures)
                .FirstAsync(t => t.Id == id);

            return tournament.TeamMatchStatistics.Any() && tournament.Fixtures.Any();
        }

        public async Task<bool> IsActive(string id)
        {
            Tournament tournament = await repository.GetByIdAsync<Tournament>(id);

            return tournament.IsActive;
        }

        public async Task<ListTeamModel> GetWinner(string id)
        {
            Tournament tournament = await repository.All<Tournament>()
                .Include(t => t.TeamMatchStatistics)
                .FirstAsync(t => t.Id == id);

            Team winner = await repository.All<Team>()
                .Include(t => t.TeamMatchStatistics)
                .Where(t => !t.TeamMatchStatistics.Any(tms => tms.Id == id && !tms.IsWinner))
                .FirstAsync();

            ListTeamModel model = mapper.Map<ListTeamModel>(winner);

            return model;
        }
    }
}
