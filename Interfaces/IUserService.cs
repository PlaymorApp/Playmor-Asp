using Playmor_Asp.DTOs;

namespace Playmor_Asp.Interfaces
{
    public interface IUserService
    {
        public ICollection<UserDTO> GetAllUsers();
        public UserDTO GetUserById(int id);
        public UserDTO GetUserByEmail(string email);
        public UserDTO GetUserByUsername(string username);
        public UserUpdateDTO UpdateUser(int id, UserUpdateDTO userDTO);
        public UserDTO DeleteUser(int id);
    }
}
