using Core.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Contracts
{
    public interface ITeamService
    {
        Task Create(CreateTeamModel model);

        Task Edit(EditTeamModel model);

        Task Delete(string id);

        Task<IEnumerable<ListTeamModel>> GetAll();

        Task<EditTeamModel> GetEditModel(string id);

        Task<TeamDetailsModel> Details(string id);

        Task RemovePlayer(string teamId, string playerId);

        Task AddPlayer(string teamId, string playerId);
    }
}
