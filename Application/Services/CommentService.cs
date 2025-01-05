using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Comment;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserService _userService;
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;
    private readonly IValidator<Comment> _validator;

    public CommentService(ICommentRepository commentRepository, IGameService gameService, IMapper mapper, IValidator<Comment> validator, IUserService userService)
    {
        _commentRepository = commentRepository;
        _gameService = gameService;
        _mapper = mapper;
        _validator = validator;
        _userService = userService;
    }

    public async Task<ServiceResult<CommentDTO?, IError>> CreateCommentAsync(CommentPostDTO commentPostDTO, int userId)
    {
        if (commentPostDTO.CommenterId != userId)
        {
            return new ServiceResult<CommentDTO?, IError> { Data = null, Errors = [new UnauthorizedError("Unauthorized request received.")] };
        }

        var comment = _mapper.Map<Comment>(commentPostDTO);

        if (!_validator.Validate(comment).IsValid)
        {
            return new ServiceResult<CommentDTO?, IError> { Data = null, Errors = [new ValidationError("comment", "Comment validation failed, check your request data.")] };
        }

        var newComment = await _commentRepository.CreateAsync(comment);

        if (newComment == null)
        {
            return new ServiceResult<CommentDTO?, IError> { Data = null, Errors = [new UnexpectedError("Failed to create new comment.")] };
        }

        var newCommentDTO = _mapper.Map<CommentDTO>(newComment);

        newCommentDTO.Commenter = _userService.GetUserById(userId);

        if (newCommentDTO.Commenter == null)
        {
            return new ServiceResult<CommentDTO?, IError> { Data = null, Errors = [new UnexpectedError("Failed to fetch commenting user data")] };
        }

        return new ServiceResult<CommentDTO?, IError> { Data = newCommentDTO };
    }

    public async Task<ServiceResult<bool, IError>> DeleteCommentAsync(int id, int userId)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new NotFoundError($"Failed to find a message with id: {id}")] };
        }

        if (comment.CommenterId != userId)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new UnauthorizedError("Unauthorized request received.")] };
        }

        var status = await _commentRepository.DeleteAsync(id);

        if (!status)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new UnexpectedError($"Failed to delete comment with id: {id}")] };
        }

        return new ServiceResult<bool, IError> { Data = true };
    }

    public async Task<ServiceResult<ICollection<CommentDTO>, IError>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        if (comments == null)
        {
            return new ServiceResult<ICollection<CommentDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected error encoutered.")] };
        }

        return new ServiceResult<ICollection<CommentDTO>, IError> { Data = FormatComments(comments) };
    }

    public async Task<ServiceResult<ICollection<CommentDTO>, IError>> GetAllCommentsByGameIdAsync(int gameId)
    {
        var serviceResult = await _gameService.GetGameAsync(gameId);

        if (!serviceResult.IsValid)
        {
            return new ServiceResult<ICollection<CommentDTO>, IError>
            {
                Data = [],
                Errors = serviceResult.Errors
            };
        }

        var comments = await _commentRepository.GetByGameIdAsync(gameId);

        if (comments == null)
        {
            return new ServiceResult<ICollection<CommentDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected error encoutered.")] };
        }

        return new ServiceResult<ICollection<CommentDTO>, IError> { Data = FormatComments(comments) };
    }

    public async Task<ServiceResult<ICollection<CommentDTO>, IError>> GetAllRepliesByCommentIdAsync(int commentId)
    {
        var comments = await _commentRepository.GetByReplyIdAsync(commentId);

        if (comments == null)
        {
            return new ServiceResult<ICollection<CommentDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected error encoutered.")] };
        }

        return new ServiceResult<ICollection<CommentDTO>, IError> { Data = FormatComments(comments) };
    }

    public async Task<ServiceResult<CommentDTO?, IError>> GetCommentByIdAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<CommentDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("id", $"Invalid id received {id} can't be lower than 1")]
            };
        }

        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return new ServiceResult<CommentDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Failed to find comment with id: {id}")]
            };
        }

        var commentDTO = _mapper.Map<CommentDTO>(comment);

        commentDTO.Commenter = _userService.GetUserById(comment.CommenterId);

        return new ServiceResult<CommentDTO?, IError> { Data = commentDTO };
    }

    public async Task<ServiceResult<CommentDTO?, IError>> UpdateCommentAsync(CommentPutDTO commentPutDTO, int userId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentPutDTO.Id);

        if (comment == null)
        {
            return new ServiceResult<CommentDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Failed to find comment with id {commentPutDTO.Id}")]
            };
        }

        if (!_validator.Validate(comment).IsValid)
        {
            return new ServiceResult<CommentDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("Comment", $"Failed to validate comment: {commentPutDTO}")]
            };
        }

        if (comment.CommenterId != userId)
        {
            return new ServiceResult<CommentDTO?, IError>
            {
                Data = null,
                Errors = [new UnauthorizedError("Unauthorized request received.")]
            };
        }

        var updatedComment = _commentRepository.UpdateAsync(comment, comment.Id);

        if (updatedComment == null)
        {
            return new ServiceResult<CommentDTO?, IError>
            {
                Data = null,
                Errors = [new UnexpectedError("Unexpected server error.")]
            };
        }

        var updatedCommentDTO = _mapper.Map<CommentDTO>(updatedComment);

        updatedCommentDTO.Commenter = _userService.GetUserById(userId);

        return new ServiceResult<CommentDTO?, IError> { Data = updatedCommentDTO };
    }

    // Assign a UserDTO to overy commentDTO for basic data like username
    // TODO: change to async once user service gets refactored
    public ICollection<CommentDTO> FormatComments(ICollection<Comment> comments)
    {
        var commentsDTO = _mapper.Map<ICollection<CommentDTO>>(comments);

        foreach (var commentDTO in commentsDTO)
        {
            commentDTO.Commenter = _userService.GetUserById(commentDTO.CommenterId);
        }

        return commentsDTO;
    }
}
