﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Services.Contracts;
using Core.ViewModels.Fixture;
using Core.ViewModels.Tournament;
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

            await repository.AddAsync(tournament);
            await repository.SaveChangesAsync();
        }

        public async Task Schedule(CreateFixtureModel[] schedule)
        {
            List<Fixture> fixtures = new List<Fixture>();

            foreach (CreateFixtureModel f in schedule)
            {
                Fixture fixture = mapper.Map<Fixture>(f);
                fixtures.Add(fixture);
            }

            await repository.AddRangeAsync(fixtures);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> IsScheduled(string id)
        {
            Tournament tournament = await repository.GetByIdAsync<Tournament>(id);

            return tournament.TeamMatchStatistics.Any() && tournament.Fixtures.Any();
        }
    }
}