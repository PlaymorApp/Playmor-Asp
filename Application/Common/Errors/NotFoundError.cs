namespace Playmor_Asp.Application.Common.Errors;

public class NotFoundError : IError
{
    public string Message { get; }
    public string ErrorCode { get; }

    public NotFoundError(string message)
    {
        Message = message;
        ErrorCode = "NOT_FOUND";
    }
}
