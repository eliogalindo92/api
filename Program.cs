using api.Configs;
using api.Context;
using api.Extensions;
using api.Interfaces;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<UsersService>(); // Register UsersService
builder.Services.AddScoped<AuthService>(); // Register AuthService
builder.Services.AddScoped<StocksService>(); // Register StocksService
builder.Services.AddScoped<CommentsService>(); // Register CommentsService
builder.Services.AddScoped<RolesService>(); // Register RolesService
builder.Services.AddScoped<PermissionsService>(); // Register PermissionsService

builder.Services.AddScoped<IUsersRepository, UsersRepository>(); // Register StocksRepository
builder.Services.AddScoped<IRolesRepository, RolesRepository>(); // Register RolesRepository
builder.Services.AddScoped<IPermissionsRepository, PermissionsRepository>(); // Register PermissionsRepository
builder.Services.AddScoped<IStocksRepository, StocksRepository>(); // Register StocksRepository
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>(); // Register CommentsRepository

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                                            sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

// Cookie-based authentication configuration
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetSection("JwtConfig").GetValue<int>("ExpirationMinutes"));
        options.SlidingExpiration = true;
        options.Cookie.Name = "session";
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Domain = builder.Configuration["AllowedHosts"] ?? string.Empty;
        options.Cookie.Path = "/";
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });


//Add authorization
builder.Services.AddCustomAuthorizationPolicies();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", corsPolicyBuilder =>
    {
        if (builder.Environment.IsDevelopment())
        {
            corsPolicyBuilder
                .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
        else
        {
            corsPolicyBuilder
                .WithOrigins(builder.Configuration["AllowedHosts"] ?? string.Empty)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    });
});

var app = builder.Build(); // Build the app after adding services

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseRouting();

// Apply the CORS policy
app.UseCors("AllowSpecificOrigin");

// Enables authentication
app.UseAuthentication();

// Enables authorization
app.UseAuthorization();

// Map controllers
app.MapGroup("/api/v1/").MapControllers();

app.Run();