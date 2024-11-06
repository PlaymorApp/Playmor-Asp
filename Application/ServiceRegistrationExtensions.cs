﻿using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Application.Services;
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
        services.AddScoped<IHashingService, HashingService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddAutoMapper(typeof(Program));
        return services;
    }
}