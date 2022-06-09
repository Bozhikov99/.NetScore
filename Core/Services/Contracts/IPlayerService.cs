using Core.ViewModels.Player;

namespace Core.Services.Contracts
{
    public interface IPlayerService
    {
        Task Create(CreatePlayerModel model);

        Task Edit(EditPlayerModel model);

        Task Delete(string id);

        Task<PlayerDetailsModel> Details(string id);

        Task<IEnumerable<ListPlayerModel>> GetAll();
    }
}
