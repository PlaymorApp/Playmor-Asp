using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Message;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<Message> _messageValidator;
    private readonly IMapper _mapper;
    public MessageService(IMessageRepository messageRepository, IValidator<Message> messageValidator, IMapper mapper, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _messageValidator = messageValidator;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<ServiceResult<MessageDTO?, IError>> CreateMessageAsync(MessagePostDTO messagePostDTO, int userId)
    {
        var message = _mapper.Map<Message>(messagePostDTO);

        var validation = _messageValidator.Validate(message);

        if (!validation.IsValid)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("message", "Invalid message received")]
            };
        }

        var newMessage = await _messageRepository.CreateAsync(message);

        if (newMessage == null)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new UnexpectedError("Failed to create message")]
            };
        }

        var mappedMessage = _mapper.Map<MessageDTO>(newMessage);

        return new ServiceResult<MessageDTO?, IError> { Data = mappedMessage };
    }

    public async Task<ServiceResult<MessageDTO?, IError>> UpdateMessageAsync(MessagePutDTO messagePutDTO, int userId)
    {
        var message = _mapper.Map<Message>(messagePutDTO);

        var validation = _messageValidator.Validate(message);

        if (!validation.IsValid)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("message", "Invalid message received")]
            };
        }

        var existingMessage = await _messageRepository.GetByIDAsync(message.Id);

        if (existingMessage == null)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Message with id {message.Id} doesn't exist")]
            };
        }

        Message? updatedMessage = null;
        // Sender can change the message contents
        if (existingMessage.SenderId == userId)
        {
            // Request claiming to be sender hadn't changed isRead
            // Then update contents
            if (existingMessage.IsRead == messagePutDTO.IsRead)
            {
                updatedMessage = await _messageRepository.UpdateAsync(message, message.Id);
            }
        }

        // Recipients can change the isRead flag
        if (existingMessage.RecipientId == userId)
        {
            // Request claiming to be recipient hadn't changed contents
            // Then update isRead
            if (existingMessage.Content == messagePutDTO.Content)
            {
                updatedMessage = await _messageRepository.UpdateAsync(message, message.Id);
            }
        }

        if (updatedMessage == null)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new UnexpectedError("Failed to update message")]
            };
        }

        var mappedMessage = _mapper.Map<MessageDTO>(updatedMessage);

        return new ServiceResult<MessageDTO?, IError> { Data = mappedMessage };
    }

    public async Task<ServiceResult<bool, IError>> DeleteMessageAsync(int id, int userId)
    {
        var serviceResult = await GetMessageByIDAsync(id, userId);

        if (serviceResult == null)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new UnexpectedError("Unexpected error occured when deleting message")]
            };
        }

        if (!serviceResult.IsValid)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = serviceResult.Errors
            };
        }

        var message = serviceResult.Data;

        // Currently on sender has the rights to delete a message sent
        // TODO: Implement a way for recipients to hide a message from their inbox
        if (message?.SenderId != userId)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new UnauthorizedError("Unauthorized request")]
            };
        }

        var status = await _messageRepository.DeleteAsync(id);

        if (!status)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new UnexpectedError("Failed to delete message")]
            };
        }

        return new ServiceResult<bool, IError> { Data = true };
    }

    public async Task<ServiceResult<MessageDTO?, IError>> GetMessageByIDAsync(int id, int userId)
    {
        if (id < 1)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("id", "id cannot be less than 1")]
            };
        }

        var message = await _messageRepository.GetByIDAsync(id);

        // Individual message details are only available to either the sender or receiver
        if (userId != message?.RecipientId && userId != message?.SenderId)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new UnauthorizedError("Unauthorized operation")]
            };
        }

        if (message == null)
        {
            return new ServiceResult<MessageDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Failed to find message with id: {id}")]
            };
        }

        var sender = _userRepository.GetByID(message.SenderId);
        var recipient = _userRepository.GetByID(message.RecipientId);

        var senderDTO = _mapper.Map<UserDTO>(sender);
        var recipientDTO = _mapper.Map<UserDTO>(recipient);

        var messageDTO = _mapper.Map<MessageDTO>(message);

        messageDTO.Sender = senderDTO;
        messageDTO.Recipient = recipientDTO;

        return new ServiceResult<MessageDTO?, IError> { Data = messageDTO };
    }

    public async Task<ServiceResult<ICollection<MessageDTO>, IError>> GetMessagesByRecipientIdAsync(int recipientId)
    {
        if (recipientId < 1)
        {
            return new ServiceResult<ICollection<MessageDTO>, IError>
            {
                Data = [],
                Errors = [new ValidationError("recipientId", "recipientId cannot be less than 1")]
            };
        }

        var recipient = _userRepository.GetByID(recipientId);

        if (recipient == null)
        {
            return new ServiceResult<ICollection<MessageDTO>, IError>
            {
                Data = [],
                Errors = [new NotFoundError($"Failed to find recipient with {recipientId} id")]
            };
        }
        var recipientDTO = _mapper.Map<UserDTO>(recipient);

        var messages = await _messageRepository.GetByRecipientIdAsync(recipientId);
        var messagesDTO = new List<MessageDTO>();
        foreach (var message in messages)
        {
            var sender = _userRepository.GetByID(message.SenderId);

            if (sender == null)
            {
                return new ServiceResult<ICollection<MessageDTO>, IError>
                {
                    Data = [],
                    Errors = [new NotFoundError($"Failed to find sender {message.SenderId}")]
                };
            }

            var senderDTO = _mapper.Map<UserDTO>(sender);
            var messageDTO = _mapper.Map<MessageDTO>(message);

            messageDTO.Sender = senderDTO;
            messageDTO.Recipient = recipientDTO;

            messagesDTO.Add(messageDTO);
        }

        return new ServiceResult<ICollection<MessageDTO>, IError> { Data = messagesDTO };
    }

    public async Task<ServiceResult<ICollection<MessageDTO>, IError>> GetMessagesBySenderIdAsync(int senderId)
    {
        if (senderId < 1)
        {
            return new ServiceResult<ICollection<MessageDTO>, IError>
            {
                Data = [],
                Errors = [new ValidationError("recipientId", "recipientId cannot be less than 1")]
            };
        }

        var sender = _userRepository.GetByID(senderId);

        if (sender == null)
        {
            return new ServiceResult<ICollection<MessageDTO>, IError>
            {
                Data = [],
                Errors = [new NotFoundError($"Failed to find sender with {senderId} id")]
            };
        }
        var senderDTO = _mapper.Map<UserDTO>(sender);

        var messages = await _messageRepository.GetBySenderIdAsync(senderId);
        var messagesDTO = new List<MessageDTO>();
        foreach (var message in messages)
        {
            var recipient = _userRepository.GetByID(message.RecipientId);

            if (recipient == null)
            {
                return new ServiceResult<ICollection<MessageDTO>, IError>
                {
                    Data = [],
                    Errors = [new NotFoundError($"Failed to find sender {message.SenderId}")]
                };
            }

            var recipientDTO = _mapper.Map<UserDTO>(recipient);
            var messageDTO = _mapper.Map<MessageDTO>(message);

            messageDTO.Sender = senderDTO;
            messageDTO.Recipient = recipientDTO;

            messagesDTO.Add(messageDTO);
        }

        return new ServiceResult<ICollection<MessageDTO>, IError> { Data = messagesDTO };
    }
}
