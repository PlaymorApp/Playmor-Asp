using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.CommentScore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class CommentScoreService : ICommentScoreService
{
    private readonly ICommentScoreRepository _commentScoreRepository;
    private readonly IValidator<CommentScore> _validator;
    private readonly IMapper _mapper;

    public CommentScoreService(ICommentScoreRepository commentScoreRepository, IValidator<CommentScore> validator, IMapper mapper)
    {
        _commentScoreRepository = commentScoreRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CommentScoreDTO?, IError>> CreateCommentScoreAsync(CommentScorePostDTO commentPostDTO, int userId)
    {
        if (commentPostDTO.UserId != userId)
        {
            return new ServiceResult<CommentScoreDTO?, IError> { Data = null, Errors = [new UnauthorizedError("Unauthorized request received.")] };
        }

        var commentScore = _mapper.Map<CommentScore>(commentPostDTO);

        var validateResult = await _validator.ValidateAsync(commentScore);

        if (!validateResult.IsValid)
        {
            var errorString = string.Join(", ", validateResult.Errors.Select(e => e.ErrorMessage));
            return new ServiceResult<CommentScoreDTO?, IError> { Data = null, Errors = [new ValidationError("commentPostDTO", $"Passed comment object is invalid: {errorString}")] };
        }

        var resultCommentScore = await _commentScoreRepository.CreateAsync(commentScore);

        if (resultCommentScore == null)
        {
            return new ServiceResult<CommentScoreDTO?, IError> { Data = null, Errors = [new UnexpectedError("Unexpected server error")] };
        }

        return new ServiceResult<CommentScoreDTO?, IError> { Data = _mapper.Map<CommentScoreDTO>(resultCommentScore) };
    }

    public async Task<ServiceResult<bool, IError>> DeleteCommentScoreAsync(int id, int userId)
    {
        if (id < 1)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new ValidationError("id", "Id can't be lower than 1")] };
        }

        if (userId < 1)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new ValidationError("userId", "Id can't be lower than 1")] };
        }

        var commentScore = await _commentScoreRepository.GetByIdAsync(id);

        if (commentScore == null)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new NotFoundError("")] };
        }

        if (commentScore.UserId != userId)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new UnauthorizedError("Unauthorized to execute method")] };
        }

        var status = await _commentScoreRepository.DeleteAsync(id);

        if (!status)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new UnexpectedError("Unexpected server error")] };
        }

        return new ServiceResult<bool, IError> { Data = true };
    }

    public async Task<ServiceResult<ICollection<CommentScoreDTO>, IError>> GetAllCommentScoresAsync()
    {
        var commentScores = await _commentScoreRepository.GetAllAsync();

        if (commentScores == null)
        {
            return new ServiceResult<ICollection<CommentScoreDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected server error")] };
        }

        return new ServiceResult<ICollection<CommentScoreDTO>, IError> { Data = _mapper.Map<ICollection<CommentScoreDTO>>(commentScores) };
    }

    public async Task<ServiceResult<ICollection<CommentScoreDTO>, IError>> GetAllCommentScoresByCommentIdAsync(int commentId)
    {
        if (commentId < 1)
        {
            return new ServiceResult<ICollection<CommentScoreDTO>, IError> { Data = [], Errors = [new ValidationError("commentId", "Id can't be lower than 1")] };
        }

        var commentScores = await _commentScoreRepository.GetByCommentIdAsync(commentId);

        if (commentScores == null)
        {
            return new ServiceResult<ICollection<CommentScoreDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected server error")] };
        }

        return new ServiceResult<ICollection<CommentScoreDTO>, IError> { Data = _mapper.Map<ICollection<CommentScoreDTO>>(commentScores) };
    }

    public async Task<ServiceResult<CommentScoreDTO?, IError>> GetCommentScoreByIdAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<CommentScoreDTO?, IError> { Data = null, Errors = [new ValidationError("id", "Id can't be lower than 1")] };
        }

        var commentScore = await _commentScoreRepository.GetByIdAsync(id);

        if (commentScore == null)
        {
            return new ServiceResult<CommentScoreDTO?, IError> { Data = null, Errors = [new NotFoundError("")] };
        }

        return new ServiceResult<CommentScoreDTO?, IError> { Data = _mapper.Map<CommentScoreDTO>(commentScore) };
    }

    // Less taxing way of doing likes is to keep a periodic job running
    // that would update a likes field in the comment table for each row.
    public async Task<ServiceResult<int, IError>> GetCommentScoreTotalAsync(int commentId)
    {
        if (commentId < 1)
        {
            return new ServiceResult<int, IError> { Data = 0, Errors = [new ValidationError("commentId", "Id can't be lower than 1")] };
        }

        var commentScores = await _commentScoreRepository.GetByCommentIdAsync(commentId);

        if (commentScores == null)
        {
            return new ServiceResult<int, IError> { Data = 0, Errors = [new UnexpectedError("Unexpected server error")] };
        }

        var total = 0;

        foreach (var commentScore in commentScores)
        {
            total += commentScore.Value;
        }

        return new ServiceResult<int, IError> { Data = total };
    }
}
