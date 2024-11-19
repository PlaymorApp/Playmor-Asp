using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs;

namespace Playmor_Asp.Application.Interfaces;

public interface IUserGameService
{
    public Task<ServiceResult<UserGameDTO?, IError>> GetUserGameByIdAsync(int id);
    public Task<ServiceResult<ICollection<UserGameDTO>, IError>> GetUserGamesByUserIdAsync(int userId);
    public Task<ServiceResult<bool, IError>> DeleteUserGameAsync(int id);
    public Task<ServiceResult<UserGameDTO?, IError>> CreateUserGameAsync(UserGamePostDTO userGamePostDTO);

}
