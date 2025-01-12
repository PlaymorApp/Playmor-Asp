using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.CommentScore;

namespace Playmor_Asp.Application.Interfaces;

public interface ICommentScoreService
{
    public Task<ServiceResult<ICollection<CommentScoreDTO>, IError>> GetAllCommentScoresAsync();
    public Task<ServiceResult<ICollection<CommentScoreDTO>, IError>> GetAllCommentScoresByCommentIdAsync(int commentId);
    public Task<ServiceResult<CommentScoreDTO?, IError>> GetCommentScoreByIdAsync(int id);
    public Task<ServiceResult<CommentScoreDTO?, IError>> CreateCommentScoreAsync(CommentScorePostDTO commentPostDTO, int userId);
    public Task<ServiceResult<bool, IError>> DeleteCommentScoreAsync(int id, int userId);
    public Task<ServiceResult<int, IError>> GetCommentScoreTotalAsync(int commentId);
}
