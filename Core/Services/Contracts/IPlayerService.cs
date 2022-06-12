using Core.ViewModels.Player;

namespace Core.Services.Contracts
{
    public interface IPlayerService
    {
        Task Create(CreatePlayerModel model);

        Task Edit(EditPlayerModel model);

        Task<EditPlayerModel> GetEditModel(string id);

        Task Delete(string id);

        Task<PlayerDetailsModel> Details(string id);

        Task<IEnumerable<ListPlayerModel>> GetAll();

        Task<IEnumerable<ListPlayerModel>> GetAll(string teamId);

        Task<IEnumerable<ListPlayerModel>> GetAll(string[] ids);

        Task<IEnumerable<ListPlayerModel>> GetFreeAgents();
    }
}
