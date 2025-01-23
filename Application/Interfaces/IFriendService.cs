using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Friend;

namespace Playmor_Asp.Application.Interfaces;

public interface IFriendService
{
    public Task<ServiceResult<ICollection<FriendDTO>, IError>> GetFriendsByUserIdAsync(int id);
    public Task<ServiceResult<FriendDTO?, IError>> GetFriendByIdAsync(int id);
    public Task<ServiceResult<FriendDTO?, IError>> CreateFriendAsync(FriendPostDTO friendPostDTO);
    public Task<ServiceResult<bool, IError>> DeleteFriendAsync(int id, int userId);
}
