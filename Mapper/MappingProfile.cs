using AutoMapper;
using Playmor_Asp.DTOs;
using Playmor_Asp.Models;

namespace Playmor_Asp.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
        CreateMap<UserUpdateDTO, User>();
        CreateMap<User, UserUpdateDTO>();
        CreateMap<UserRegisterDTO, User>();
        CreateMap<User, UserRegisterDTO>();
        CreateMap<UserCredentialsDTO, User>();
        CreateMap<User, UserCredentialsDTO>();
    }
}
