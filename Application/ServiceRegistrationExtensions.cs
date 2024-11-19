using FluentValidation;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Application.Services;
using Playmor_Asp.Application.Validators;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Repositories;
using Playmor_Asp.Infrastructure.Seeders;
using Playmor_Asp.Presentation.Middlewares;

namespace Playmor_Asp.Application;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<GameSeed>();
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddTransient<HttpRequestLoggingMiddleware>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserGameService, UserGameService>();
        services.AddScoped<IUserGameRepository, UserGameRepository>();
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IValidator<Game>, GameValidator>();
        services.AddScoped<IValidator<User>, UserValidator>();
        services.AddScoped<IValidator<UserGame>, UserGameValidator>();
        services.AddAutoMapper(typeof(Program));
        return services;
    }
}
