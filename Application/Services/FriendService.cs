using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Friend;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class FriendService : IFriendService
{
    private readonly IFriendRepository _friendRepository;
    private readonly IValidator<Friend> _validator;
    private readonly IMapper _mapper;

    public FriendService(IFriendRepository friendRepository, IValidator<Friend> validator, IMapper mapper)
    {
        _friendRepository = friendRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ServiceResult<FriendDTO?, IError>> CreateFriendAsync(FriendPostDTO friendPostDTO)
    {
        var mappedFriend = _mapper.Map<Friend>(friendPostDTO);

        var validateStatus = await _validator.ValidateAsync(mappedFriend);

        if (!validateStatus.IsValid)
        {
            var errorsString = string.Join(" ", validateStatus.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));

            return new ServiceResult<FriendDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError(nameof(friendPostDTO), $"Validation failed: {errorsString}")]
            };
        }

        var newFriend = await _friendRepository.CreateAsync(mappedFriend);

        if (newFriend is null)
        {
            return new ServiceResult<FriendDTO?, IError> { Data = null, Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        var newFriendMapped = _mapper.Map<FriendDTO>(newFriend);

        return new ServiceResult<FriendDTO?, IError> { Data = newFriendMapped };
    }

    public async Task<ServiceResult<bool, IError>> DeleteFriendAsync(int id, int userId)
    {
        if (id < 1)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new ValidationError(nameof(id), $"Validation failed: {nameof(id)} can't be lower than 1")] };
        }

        var friendToDelete = await _friendRepository.GetByIdAsync(id);

        if (friendToDelete is null)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new NotFoundError($"Failed to find friend entry with id: {id}")] };
        }

        if (friendToDelete.FirstUserId != userId && friendToDelete.SecondUserId != userId)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new UnauthorizedError("Unauthorized to perform operation.")] };
        }

        var status = await _friendRepository.DeleteAsync(id);

        if (!status)
        {
            return new ServiceResult<bool, IError> { Data = false, Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        return new ServiceResult<bool, IError> { Data = true };
    }

    public async Task<ServiceResult<FriendDTO?, IError>> GetFriendByIdAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<FriendDTO?, IError> { Data = null, Errors = [new ValidationError(nameof(id), $"Validation failed: {nameof(id)} can't be lower than 1")] };
        }

        var friend = await _friendRepository.GetByIdAsync(id);

        if (friend is null)
        {
            return new ServiceResult<FriendDTO?, IError> { Data = null, Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        var mappedFriend = _mapper.Map<FriendDTO>(friend);

        return new ServiceResult<FriendDTO?, IError> { Data = mappedFriend };
    }

    public async Task<ServiceResult<ICollection<FriendDTO>, IError>> GetFriendsByUserIdAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<ICollection<FriendDTO>, IError> { Data = [], Errors = [new ValidationError(nameof(id), $"Validation failed: {nameof(id)} can't be lower than 1")] };
        }

        var friends = await _friendRepository.GetByUserIdAsync(id);

        if (friends is null)
        {
            return new ServiceResult<ICollection<FriendDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        var mappedFriends = _mapper.Map<ICollection<FriendDTO>>(friends);

        return new ServiceResult<ICollection<FriendDTO>, IError> { Data = mappedFriends };
    }
}
