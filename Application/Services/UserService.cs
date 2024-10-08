using AutoMapper;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public ICollection<UserDTO> GetAllUsers()
    {
        var users = _userRepository.GetAll();
        return _mapper.Map<ICollection<UserDTO>>(users);
    }

    public UserDTO GetUserById(int id)
    {
        if (id < 1) throw new ArgumentException($"Invalid id argument, passed id: {id}. Must be greater than 1");
        var user = _userRepository.GetByID(id);
        if (user == null) throw new Exception($"User with ${id} id not found");
        return _mapper.Map<UserDTO>(user);
    }

    public UserDTO GetUserByEmail(string email)
    {
        var user = _userRepository.GetByEmail(email);
        if (user == null) throw new Exception($"User with ${email} email not found");
        return _mapper.Map<UserDTO>(user);
    }

    public UserDTO GetUserByUsername(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null) throw new Exception($"User with ${username} username not found");
        return _mapper.Map<UserDTO>(user);
    }

    public UserUpdateDTO UpdateUser(int id, UserUpdateDTO userDTO)
    {
        if (id < 1) throw new ArgumentException($"Invalid argument {id}. Must be greater or equal 1");
        _ = _userRepository.GetByID(id) ?? throw new Exception($"User with id {id} not found");
        var mappedUser = _mapper.Map<User>(userDTO);
        var updatedUser = _userRepository.Update(id, mappedUser, typeof(UserUpdateDTO));
        return _mapper.Map<UserUpdateDTO>(updatedUser);
    }

    public UserDTO DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}
