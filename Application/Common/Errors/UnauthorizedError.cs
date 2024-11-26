namespace Playmor_Asp.Application.Common.Errors;

public class UnauthorizedError : IError
{
    public string Message { get; }
    public string ErrorCode { get; }

    public UnauthorizedError(string message)
    {
        Message = message;
        ErrorCode = "Unauthorized action error";
    }
}
