﻿using AutoMapper;
using Playmor_Asp.Application.DTOs.Comment;
using Playmor_Asp.Application.DTOs.CommentScore;
using Playmor_Asp.Application.DTOs.Friend;
using Playmor_Asp.Application.DTOs.Game;
using Playmor_Asp.Application.DTOs.Message;
using Playmor_Asp.Application.DTOs.Notification;
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

        CreateMap<GamePostDTO, Game>();
        CreateMap<Game, GamePostDTO>();

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

        CreateMap<MessagePatchDTO, Message>();
        CreateMap<Message, MessagePatchDTO>();

        CreateMap<MessagePatchDTO, MessageDTO>();
        CreateMap<MessageDTO, MessagePatchDTO>();

        CreateMap<Comment, CommentDTO>();
        CreateMap<CommentDTO, Comment>();

        CreateMap<CommentPutDTO, Comment>();
        CreateMap<Comment, CommentPutDTO>();

        CreateMap<CommentPostDTO, Comment>();
        CreateMap<Comment, CommentPostDTO>();

        CreateMap<CommentScore, CommentScoreDTO>();
        CreateMap<CommentScoreDTO, CommentScore>();

        CreateMap<CommentScorePostDTO, CommentScore>();
        CreateMap<CommentScore, CommentScorePostDTO>();

        CreateMap<Friend, FriendDTO>();
        CreateMap<FriendDTO, Friend>();

        CreateMap<FriendPostDTO, Friend>();
        CreateMap<Friend, FriendPostDTO>();

        CreateMap<Notification, NotificationDTO>();
        CreateMap<NotificationDTO, Notification>();

        CreateMap<NotificationPostDTO, Notification>();
        CreateMap<Notification, NotificationPostDTO>();
    }
}
