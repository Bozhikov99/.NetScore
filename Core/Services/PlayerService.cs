using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Services.Contracts;
using Core.ViewModels.Player;
using Infrastructure.Common;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public PlayerService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task Create(CreatePlayerModel model)
        {
            Player player = mapper.Map<Player>(model);

            await repository.AddAsync(player);
            await repository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            await repository.DeleteAsync<Player>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<PlayerDetailsModel> Details(string id)
        {
            Player player = await repository.GetByIdAsync<Player>(id);
            PlayerDetailsModel details = mapper.Map<PlayerDetailsModel>(player);

            return details;
        }

        public async Task Edit(EditPlayerModel model)
        {
            Player player = mapper.Map<Player>(model);

            repository.Update(player);
            await repository.SaveChangesAsync();
        }

        public async Task<EditPlayerModel> GetEditModel(string id)
        {
            Player player = await repository.GetByIdAsync<Player>(id);
            EditPlayerModel model = mapper.Map<EditPlayerModel>(player);

            return model;
        }

        public async Task<IEnumerable<ListPlayerModel>> GetAll()
        {
            IEnumerable<ListPlayerModel> players = await repository.All<Player>()
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return players;
        }

        public async Task<IEnumerable<ListPlayerModel>> GetAll(string teamId)
        {
            IEnumerable<ListPlayerModel> players = await repository.All<Player>(p => p.TeamId == teamId)
                .OrderBy(p => p.Position)
                .ThenBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return players;
        }

        public async Task<IEnumerable<ListPlayerModel>> GetAll(string[] ids)
        {
            IEnumerable<ListPlayerModel> players = await repository.All<Player>(p => ids.Contains(p.Id))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.FirstName)
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return players;
        }

        public async Task<IEnumerable<ListPlayerModel>> GetFreeAgents()
        {
            IEnumerable<ListPlayerModel> players = await repository.All<Player>(p => p.TeamId == null)
                .ProjectTo<ListPlayerModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return players;
        }

    }
}
