using Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListModel>> GetUsers();

        Task<IdentityResult?> Register(RegisterUserModel model);

        Task Login(LoginUserModel model);

        string GetUserId();

        Task<bool> IsAdmin();
    }
}
