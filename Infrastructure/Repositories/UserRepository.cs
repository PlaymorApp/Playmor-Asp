using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;
    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IEnumerable<User> GetAll()
    {
        return _dataContext.Users;
    }
    public User? GetByID(int userID)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Id == userID);
    }

    public User? GetByEmail(string email)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetByUsername(string username)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? Create(User user)
    {
        var newUser = _dataContext.Users.Add(user).Entity;

        try
        {
            var saved = Save();
            if (!saved) return null;

            return newUser;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public User? Update(int userID, User user, Type _)
    {
        var oldUser = GetByID(userID);
        if (oldUser == null)
        {
            return null;
        }

        // Generic Type is explained in CopyProperties method below.
        CopyProperties<Type>(user, oldUser);

        try
        {
            var saved = Save();
            if (!saved) return null;

            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public bool Delete(int userID)
    {
        var user = GetByID(userID);
        if (user == null)
        {
            return false;
        }

        _dataContext.Remove(user);

        try
        {
            var saved = Save();
            return saved;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Save()
    {
        return _dataContext.SaveChanges() > 0;
    }

    // Using the generic <T> allows copying over only properties in a user based DTO,
    // without having to worry about any potential null overwrites.
    public void CopyProperties<T>(User userSource, User userDestination)
    {
        var properties = typeof(T).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
        foreach (var property in properties)
        {
            var value = property.GetValue(userSource, null);
            if (value != null) property.SetValue(userDestination, value, null);
        }
    }
}
