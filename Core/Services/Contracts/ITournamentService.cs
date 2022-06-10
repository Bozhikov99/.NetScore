﻿using Core.ViewModels.Fixture;
using Core.ViewModels.Tournament;

namespace Core.Services.Contracts
{
    public interface ITournamentService
    {
        Task Create(CreateTournamentModel model);
        Task Schedule(CreateFixtureModel[] schedule);

        Task<bool> IsScheduled(string id);

        Task<IEnumerable<ListTournamentModel>> GetAll();
        //Task Create(CreateTournamentModel model);
    }
}
