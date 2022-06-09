using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Services.Contracts;
using Core.ViewModels.Team;
using Infrastructure.Common;
using Infrastructure.Models;
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

        public async Task AddPlayer(string teamId, string playerId)
        {
            Team team = await repository.GetByIdAsync<Team>(teamId);
            Player player = await repository.GetByIdAsync<Player>(playerId);

            ArgumentNullException.ThrowIfNull(player, "x");
            ArgumentNullException.ThrowIfNull(team, "x");

            team.Players.Add(player);
            player.Team = team;

            repository.Update(player);
            repository.Update(team);
            await repository.SaveChangesAsync();
        }

        public async Task Create(CreateTeamModel model)
        {
            Team team = mapper.Map<Team>(model);

            foreach (string playerId in model.PlayerIds)
            {
                Player currentPlayer = await repository.GetByIdAsync<Player>(playerId);
                currentPlayer.Team = team;
                team.Players.Add(currentPlayer);
            }

            await repository.AddAsync(team);
            await repository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
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
    }
}
