﻿using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class UserGameRepository : IUserGameRepository
{
    private readonly DataContext _dataContext;
    public UserGameRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<UserGame?> CreateAsync(UserGame userGame)
    {
        var newUserGame = (await _dataContext.UserGames.AddAsync(userGame)).Entity;
        if (newUserGame == null) return null;
        var saved = await SaveAsync();
        return saved ? newUserGame : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userGame = await GetByIDAsync(id);
        if (userGame == null)
        {
            return false;
        }

        _dataContext.Remove(userGame);

        return await SaveAsync();
    }

    public async Task<UserGame?> GetByIDAsync(int id)
    {
        return await _dataContext.UserGames.Include(ug => ug.Game).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<UserGame>> GetByUserIDAsync(int userId)
    {
        return await _dataContext.UserGames
            .Include(ug => ug.Game)
            .Where(ug => ug.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var entriesWritten = await _dataContext.SaveChangesAsync();
        return entriesWritten > 0;
    }
}
