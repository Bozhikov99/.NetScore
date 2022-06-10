using Common;
using Core.Mapping;
using Core.Services;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.ModelBinders.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NetScoreDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<NetScoreDbContext>();

//Automapper Profiles
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<PlayerProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TeamProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TournamentProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<FixtureProfile>());

//Services & Repository
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();

builder.Services.AddControllersWithViews()
        .AddMvcOptions(options =>
        {
            options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(DateTimeFormatConstant.Format));
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
