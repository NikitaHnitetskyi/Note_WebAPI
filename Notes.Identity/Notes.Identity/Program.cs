using Microsoft.AspNetCore.Identity;
using Notes.Identity;
using Notes.Identity.Data;
using Notes.Identity.Model;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = AppConfi

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Notes.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/logout";
});
var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.UseIdentityServer();
app.Run();
