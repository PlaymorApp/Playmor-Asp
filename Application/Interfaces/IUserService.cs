using Playmor_Asp.Application.DTOs.User;

namespace Playmor_Asp.Application.Interfaces;

public interface IUserService
{
    public ICollection<UserDTO> GetAllUsers();
    public UserDTO GetUserById(int id);
    public UserDTO GetUserByEmail(string email);
    public UserDTO GetUserByUsername(string username);
    public UserPutDTO UpdateUser(int id, UserPutDTO userDTO);
    public UserDTO DeleteUser(int id);
}
