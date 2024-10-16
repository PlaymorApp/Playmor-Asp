namespace Playmor_Asp.Application.Common.Errors;

public class UnexpectedError : IError
{
    public string Message { get; }
    public string ErrorCode { get; }

    public UnexpectedError(string message)
    {
        Message = message;
        ErrorCode = "Unexpected server error";
    }
}
