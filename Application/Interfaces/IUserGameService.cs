using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.Common.Types;
using Playmor_Asp.Application.DTOs.UserGame;

namespace Playmor_Asp.Application.Interfaces;

public interface IUserGameService
{
    public Task<ServiceResult<UserGameDTO?, IError>> GetUserGameByIdAsync(int id, int userId);
    public Task<ServiceResult<ICollection<UserGameDTO>, IError>> GetUserGamesByUserIdAsync(int clientId);
    public Task<ServiceResult<bool, IError>> DeleteUserGameAsync(int id, int userId);
    public Task<ServiceResult<UserGameDTO?, IError>> CreateUserGameAsync(UserGamePostDTO userGamePostDTO);
    public Task<ServiceResult<UserGameStatistics?, IError>> GetUserGamesStatisticsAsync(int userId);
}
