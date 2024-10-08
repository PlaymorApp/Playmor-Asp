using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IUserRepository
{
    public IEnumerable<User> GetAll();
    public User? GetByID(int userID);
    public User? GetByUsername(string username);
    public User? GetByEmail(string email);
    public User? Create(User user);
    public User? Update(int userID, User user, Type _);
    public bool Delete(int userID);
    public bool Save();
}
