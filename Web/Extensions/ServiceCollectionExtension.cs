using Core.Mapping;
using Core.Services;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<NetScoreDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<PlayerProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<TeamProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<TournamentProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<FixtureProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<TeamMatchStatisticProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<PlayerMatchStatisticProfile>());
            services.AddAutoMapper(cfg => cfg.AddProfile<UserProfile>());

            return services;
        }
    }
}
