using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Comment;

namespace Playmor_Asp.Application.Interfaces;

public interface ICommentService
{
    public Task<ServiceResult<ICollection<CommentDTO>, IError>> GetAllCommentsAsync();
    public Task<ServiceResult<ICollection<CommentDTO>, IError>> GetAllCommentsByGameIdAsync(int gameId);
    public Task<ServiceResult<CommentDTO?, IError>> GetCommentByIdAsync(int id);
    public Task<ServiceResult<CommentDTO?, IError>> CreateCommentAsync(CommentPostDTO commentPostDTO, int userId);
    public Task<ServiceResult<CommentDTO?, IError>> UpdateCommentAsync(CommentPutDTO commentPutDTO, int userId);
    public Task<ServiceResult<bool, IError>> DeleteCommentAsync(int id, int userId);
}
