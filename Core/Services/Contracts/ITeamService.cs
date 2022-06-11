using Core.ViewModels.Player;
using Core.ViewModels.Team;

namespace Core.Services.Contracts
{
    public interface ITeamService
    {
        Task Create(CreateTeamModel model);

        Task Edit(EditTeamModel model);

        Task Delete(string id);

        Task<IEnumerable<ListTeamModel>> GetAll();

        Task<IEnumerable<ListTeamModel>> GetTeamsForTournament(string id);

        Task<IEnumerable<ListPlayerModel>> GetPlayers(string id);

        Task<EditTeamModel> GetEditModel(string id);

        Task<TeamDetailsModel> Details(string id);

        Task RemovePlayer(string teamId, string playerId);

        Task AddToSquad(string[] playerIds, string teamId);

        Task<IEnumerable<ListTeamModel>> GetCompleteTeams();
    }
}
