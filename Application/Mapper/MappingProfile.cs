using AutoMapper;
using Playmor_Asp.Application.DTOs.Comment;
using Playmor_Asp.Application.DTOs.Game;
using Playmor_Asp.Application.DTOs.Message;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Application.DTOs.UserGame;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();

        CreateMap<UserPutDTO, User>();
        CreateMap<User, UserPutDTO>();

        CreateMap<UserPostDTO, User>();
        CreateMap<User, UserPostDTO>();

        CreateMap<UserTokenDTO, User>();
        CreateMap<User, UserTokenDTO>();

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

        CreateMap<MessagePutDTO, Message>();
        CreateMap<Message, MessagePutDTO>();

        CreateMap<Comment, CommentDTO>();
        CreateMap<CommentDTO, Comment>();

        CreateMap<CommentPutDTO, Comment>();
        CreateMap<Comment, CommentPutDTO>();

        CreateMap<CommentPostDTO, Comment>();
        CreateMap<Comment, CommentPostDTO>();
    }
}
