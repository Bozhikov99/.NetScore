using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Services.Contracts;
using Core.ViewModels.Player;
using Core.ViewModels.Team;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public TeamService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ListTeamModel>> GetCompleteTeams()
        {
            IEnumerable<ListTeamModel> teams = await repository.All<Team>(t => t.Players.Count >= 11
            && t.Players.Any(p => p.Position == Position.GoalKeeper))
                .ProjectTo<ListTeamModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return teams;
        }

        public async Task Create(CreateTeamModel model)
        {
            Team team = mapper.Map<Team>(model);

            await TransferPlayers(team, model.PlayerIds);

            await repository.AddAsync(team);
            await repository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Player[] players = await repository.All<Player>(p => p.TeamId == id)
                .ToArrayAsync();

            foreach (Player player in players)
            {
                player.TeamId = null;
            }

            repository.UpdateRange(players);

            await repository.DeleteAsync<Team>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<TeamDetailsModel> Details(string id)
        {
            Team team = await repository.GetByIdAsync<Team>(id);
            TeamDetailsModel details = mapper.Map<TeamDetailsModel>(team);

            return details;
        }

        public async Task Edit(EditTeamModel model)
        {
            Team team = mapper.Map<Team>(model);

            repository.Update(team);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ListTeamModel>> GetAll()
        {
            IEnumerable<ListTeamModel> teams = await repository.All<Team>()
                .ProjectTo<ListTeamModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return teams;
        }

        public async Task<IEnumerable<ListTeamModel>> GetTeamsForTournament(string id)
        {
            var tournament = await repository.All<Tournament>()
                .Include(t => t.Teams)
                .FirstAsync(t => t.Id == id);

            IEnumerable<ListTeamModel> teams = tournament.Teams
                .AsQueryable()
                .ProjectTo<ListTeamModel>(mapper.ConfigurationProvider);

            return teams;
        }

        public async Task<IEnumerable<ListPlayerModel>> GetPlayers(string id)
        {
            IEnumerable<ListPlayerModel> players = await repository.All<Player>(p=>p.TeamId==id)
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return players;
        }

        public async Task<EditTeamModel> GetEditModel(string id)
        {
            Team team = await repository.GetByIdAsync<Team>(id);
            EditTeamModel model = mapper.Map<EditTeamModel>(team);

            return model;
        }

        public async Task RemovePlayer(string teamId, string playerId)
        {
            Team team = await repository.GetByIdAsync<Team>(teamId);
            Player player = await repository.GetByIdAsync<Player>(playerId);

            ArgumentNullException.ThrowIfNull(player, "x");
            ArgumentNullException.ThrowIfNull(team, "x");

            team.Players.Remove(player);

            repository.Update(player);
            repository.Update(team);
            await repository.SaveChangesAsync();
        }

        public async Task AddToSquad(string[] playerIds, string teamId)
        {
            Team team = await repository.GetByIdAsync<Team>(teamId);

            await TransferPlayers(team, playerIds);

            repository.Update(team);
            await repository.SaveChangesAsync();
        }


        private async Task TransferPlayers(Team team, string[] playerIds)
        {
            foreach (string playerId in playerIds)
            {
                Player currentPlayer = await repository.GetByIdAsync<Player>(playerId);
                currentPlayer.Team = team;
                team.Players.Add(currentPlayer);
            }
        }
    }
}
