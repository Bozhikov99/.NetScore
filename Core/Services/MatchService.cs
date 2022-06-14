using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.ViewModels.Match;
using Core.ViewModels.Player;
using Core.ViewModels.PlayerMatchStatistic;
using Core.ViewModels.Team;
using Infrastructure.Common;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public MatchService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task CreateMatch(CreateMatchModel model)
        {
            string homeTeamId = model.HomeTeamStatistics.TeamId;
            string awayTeamId = model.AwayTeamStatistics.TeamId;
            string matchCode = model.MatchCode;

            Fixture fixture = await repository.All<Fixture>(f => f.MatchCode == matchCode)
                .FirstAsync();

            await repository.DeleteAsync<Fixture>(fixture.Id);

            Tournament tournament = await repository.GetByIdAsync<Tournament>(fixture.TournamentId);

            List<PlayerMatchStatistic> homePlayerMatchStatistics = new List<PlayerMatchStatistic>();
            List<PlayerMatchStatistic> awayPlayerMatchStatistics = new List<PlayerMatchStatistic>();

            foreach (CreatePlayerMatchStatisticModel m in model.HomePlayerMatchStatistics
                .Where(p => p.TeamId == homeTeamId))
            {
                PlayerMatchStatistic currentPlayerStat = mapper.Map<PlayerMatchStatistic>(m);
                homePlayerMatchStatistics.Add(currentPlayerStat);
            }

            foreach (CreatePlayerMatchStatisticModel m in model.AwayPlayerMatchStatistics
                .Where(p => p.TeamId == awayTeamId))
            {
                PlayerMatchStatistic currentPlayerStat = mapper.Map<PlayerMatchStatistic>(m);
                awayPlayerMatchStatistics.Add(currentPlayerStat);
            }

            TeamMatchStatistic homeStat = mapper.Map<TeamMatchStatistic>(model.HomeTeamStatistics);
            TeamMatchStatistic awayStat = mapper.Map<TeamMatchStatistic>(model.AwayTeamStatistics);

            homeStat.PlayerMatchStatistics = homePlayerMatchStatistics;
            awayStat.PlayerMatchStatistics = awayPlayerMatchStatistics;

            await repository.AddRangeAsync(homePlayerMatchStatistics);
            await repository.AddRangeAsync(awayPlayerMatchStatistics);
            await repository.SaveChangesAsync();
            await repository.AddAsync(homeStat);
            await repository.AddAsync(awayStat);
            await repository.SaveChangesAsync();

            if (await IsWon(tournament))
            {
                await Win(tournament);
            }
        }

        public async Task<LoadedMatchModel> LoadMatch(string[] homeIds, string[] awayIds, string tournamentId)
        {
            IEnumerable<ListPlayerModel> homePlayers = await repository.All<Player>(p => homeIds.Contains(p.Id))
                .OrderBy(p => p.Position)
                .ThenBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            IEnumerable<ListPlayerModel> awayPlayers = await repository.All<Player>(p => awayIds.Contains(p.Id))
                .OrderBy(p => p.Position)
                .ThenBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            string homeTeamName = homePlayers.First().Team;
            string awayTeamName = awayPlayers.First().Team;

            Team homeTeam = await repository.All<Team>(t => t.Name == homeTeamName)
                .FirstAsync();

            ListTeamModel homeTeamModel = mapper.Map<ListTeamModel>(homeTeam);

            Team awayTeam = await repository.All<Team>(t => t.Name == awayTeamName)
                .FirstAsync();

            ListTeamModel awayTeamModel = mapper.Map<ListTeamModel>(awayTeam);

            Fixture fixture = await repository.All<Fixture>(f => f.AwayTeam.Name == awayTeamName && f.HomeTeam.Name == homeTeamName)
                .FirstAsync();

            string matchCode = fixture.MatchCode;

            LoadedMatchModel match = new LoadedMatchModel()
            {
                HomePlayers = homePlayers,
                AwayPlayers = awayPlayers,
                HomeTeam = homeTeamModel,
                AwayTeam = awayTeamModel,
                TournamentId = tournamentId,
                MatchCode = matchCode
            };

            return match;
        }

        private async Task<bool> IsWon(Tournament tournament)
        {
            var tournamentTeams = await repository.All<Team>()
                .Include(t => t.TeamMatchStatistics)
                .ToArrayAsync();

            IEnumerable<Team> undefeatedTeams = await repository.All<Team>(
                t => !t.TeamMatchStatistics.Any(tms => !tms.IsWinner
                    && tms.TournamentId == tournament.Id))
                .ToArrayAsync();

            return undefeatedTeams.Count() == 1;
        }

        private async Task Win(Tournament tournament)
        {
            tournament.IsActive = false;
            Team winner = await repository.All<Team>()
                .FirstAsync(t => !t.TeamMatchStatistics.Any(tms => !tms.IsWinner && tms.TournamentId == tournament.Id));

            winner.Trophies += 1;

            await repository.SaveChangesAsync();
        }
    }
}
