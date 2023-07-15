global using FastEndpoints;
global using FastEndpoints.Security;
using DataLayer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.EfCore;
using Services.External;
using WebApi;

var builder = WebApplication.CreateBuilder();

string? tokenSigningKey = builder.Configuration["TokenSigningKeys:Application"];

if (tokenSigningKey == null)
{
    throw new ArgumentNullException(nameof(tokenSigningKey));
}

builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer(tokenSigningKey);

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseMySql(builder.Configuration["ConnectionStrings:foodJournal"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.6.44-mysql"));
});

builder.Services.AddDbContext<sewright22_foodjournalContext>(optionsBuilder =>
{
    optionsBuilder.UseMySql(builder.Configuration["ConnectionStrings:foodJournal"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.6.44-mysql"));
});

builder.Services.Configure<TandemApiOptions>(builder.Configuration.GetSection("TandemApi"));
builder.Services.Configure<FitbitApiOptions>(builder.Configuration.GetSection("Fitbit"));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJournalService, JournalService>();
builder.Services.AddTransient<IFitbitService, FitbitService>();
builder.Services.AddTransient<IInsulinPumpDataService, TandemDataService>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddHttpClient<TandemDataService>();
builder.Services.AddHttpClient<FitbitService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();