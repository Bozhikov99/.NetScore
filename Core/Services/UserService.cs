using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.ErrorMessageConstants;
using Core.Services.Contracts;
using Core.ViewModels.User;
using Infrastructure.Common;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(
            IRepository repository,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            RoleManager<IdentityRole> roleManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<UserListModel>> GetUsers()
        {
            IEnumerable<UserListModel> users = await repository.All<User>()
                .ProjectTo<UserListModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return users;
        }

        public string GetUserId()
        {
            string userId = httpContextAccessor.HttpContext
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return userId;
        }

        public async Task Login(LoginUserModel model)
        {
            string username = model.UserName;

            User? user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new ArgumentException(UserErrorConstants.INVALID_USER);
            }

            bool isValidPassword = await userManager.CheckPasswordAsync(user, model.Password);

            if (!isValidPassword)
            {
                throw new ArgumentException(UserErrorConstants.INVALID_USER);
            }

            await signInManager.SignInAsync(user, true);
        }

        public async Task<IdentityResult?> Register(RegisterUserModel model)
        {
            User user = mapper.Map<User>(model);
            User existingUser = await repository.All<User>().FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (existingUser != null)
            {
                throw new ArgumentException(UserErrorConstants.USER_EXISTS);
            }
            IdentityResult? result = await userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<bool> IsAdmin()
        {
            string userId = GetUserId();

            User user = await repository.GetByIdAsync<User>(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, RoleConstants.ADMINISTRATOR);

            return isAdmin;
        }
    }
}
