using AutoMapper;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Mapper;

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

        CreateMap<Game, GameDTO>();
        CreateMap<GameDTO, Game>();

        CreateMap<UserGame, UserGameDTO>();
        CreateMap<UserGameDTO, UserGame>();

        CreateMap<UserGame, UserGamePostDTO>();
        CreateMap<UserGamePostDTO, UserGame>();

        CreateMap<Message, MessageDTO>();
        CreateMap<MessageDTO, Message>();

        CreateMap<MessagePostDTO, MessageDTO>();
        CreateMap<MessageDTO, MessagePostDTO>();

        CreateMap<MessagePostDTO, Message>();
        CreateMap<Message, MessagePostDTO>();
    }
}
