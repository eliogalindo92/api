using api.Configs;
using api.Context;
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

builder.Services.AddScoped<IUsersRepository, UsersRepository>(); // Register StocksRepository
builder.Services.AddScoped<IStocksRepository, StocksRepository>(); // Register StocksRepository
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>(); // Register CommentsRepository

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

// Cookie-based authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetSection("JwtConfig").GetValue<int>("ExpirationMinutes"));
        options.SlidingExpiration = true; // Extends expiration time on each request
        options.Cookie.Name = "session";
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Domain = builder.Configuration["AllowedHosts"] ?? string.Empty; // Set the cookie domain
        options.Cookie.Path = "/"; // Set the cookie path
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Use SameAsRequest for development
    });

//Add authorization
builder.Services.AddAuthorization();
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins(builder.Configuration["AllowedHosts"] ?? string.Empty)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
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

// Use HttpsRedirection is used to redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enables authentication
app.UseAuthentication();

// Enables authorization
app.UseAuthorization();

// Map controllers
app.MapGroup("/api/v1/").MapControllers();

app.Run();