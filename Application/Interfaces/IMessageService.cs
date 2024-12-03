using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Message;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IMessageService
{
    public Task<ServiceResult<MessageDTO?, IError>> GetMessageByIDAsync(int id, int userId);
    public Task<ServiceResult<ICollection<MessageDTO>, IError>> GetMessagesByRecipientIdAsync(int recipientId, int userId);
    public Task<ServiceResult<ICollection<MessageDTO>, IError>> GetMessagesBySenderIdAsync(int senderId, int userId);
    public Task<ServiceResult<Message?, IError>> CreateMessageAsync(MessagePostDTO messagePostDTO, int userId);
    public Task<ServiceResult<Message?, IError>> UpdateMessageAsync(MessagePutDTO messagePutDTO, int userId);
    public Task<ServiceResult<bool, IError>> DeleteMessageAsync(int id, int userId);
}
