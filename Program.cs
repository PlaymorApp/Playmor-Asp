using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Application.Services;
using Playmor_Asp.Infrastructure.Data;
using Playmor_Asp.Infrastructure.Repositories;
using Playmor_Asp.Infrastructure.Seeders;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<GameSeed>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration
                .GetSection("AppSettings:Token").Value ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };

        // Custom event to extract token from the cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Check if token exists in the cookies
                var token = context.HttpContext.Request.Cookies["authToken"];

                // If a token exists in the cookie, use it
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });



var app = builder.Build();

if (args.Length == 1 && args[0].Equals("seeddata", StringComparison.CurrentCultureIgnoreCase))
{
    Console.WriteLine("Seeding Data");
    SeedData(app);
}
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory?.CreateScope())
    {
        var service = scope?.ServiceProvider.GetService<GameSeed>();
        service?.GameSeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(builder =>
{
    builder.WithOrigins("https://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Allow cookies and credentials
});
app.Run();
