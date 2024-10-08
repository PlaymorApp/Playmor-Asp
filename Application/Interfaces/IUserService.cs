using Playmor_Asp.Application.DTOs;

namespace Playmor_Asp.Application.Interfaces;

public interface IUserService
{
    public ICollection<UserDTO> GetAllUsers();
    public UserDTO GetUserById(int id);
    public UserDTO GetUserByEmail(string email);
    public UserDTO GetUserByUsername(string username);
    public UserUpdateDTO UpdateUser(int id, UserUpdateDTO userDTO);
    public UserDTO DeleteUser(int id);
}
