﻿using AutoMapper;
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

            List<PlayerMatchStatistic> homePlayerMatchStatistics = await model.HomePlayerMatchStatistics
                .AsQueryable()
                .ProjectTo<PlayerMatchStatistic>(mapper.ConfigurationProvider)
                .ToListAsync();

            List<PlayerMatchStatistic> awayPlayerMatchStatistics = await model.AwayPlayerMatchStatistics
                .AsQueryable()
                .ProjectTo<PlayerMatchStatistic>(mapper.ConfigurationProvider)
                .ToListAsync();

            //foreach (CreatePlayerMatchStatisticModel m in model.PlayerMatchStatistics
            //    .Where(p => p.TeamId == homeTeamId))
            //{
            //    PlayerMatchStatistic currentPlayerStat = mapper.Map<PlayerMatchStatistic>(m);
            //    homePlayerMatchStatistics.Add(currentPlayerStat);
            //}

            //foreach (CreatePlayerMatchStatisticModel m in model.PlayerMatchStatistics
            //    .Where(p => p.TeamId == awayTeamId))
            //{
            //    PlayerMatchStatistic currentPlayerStat = mapper.Map<PlayerMatchStatistic>(m);
            //    awayPlayerMatchStatistics.Add(currentPlayerStat);
            //}

            //await repository.AddRangeAsync(homePlayerMatchStatistics);
            //await repository.AddRangeAsync(awayPlayerMatchStatistics);

            TeamMatchStatistic homeStat = mapper.Map<TeamMatchStatistic>(model.HomeTeamStatistics);
            TeamMatchStatistic awayStat = mapper.Map<TeamMatchStatistic>(model.AwayTeamStatistics);

            homeStat.PlayerMatchStatistics = homePlayerMatchStatistics;
            awayStat.PlayerMatchStatistics = awayPlayerMatchStatistics;

            await repository.AddAsync(homeStat);
            await repository.AddAsync(awayStat);

            await repository.SaveChangesAsync();
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
    }
}
