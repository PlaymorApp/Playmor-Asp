namespace Playmor_Asp.Application.Common.Errors;

public interface IError
{
    string Message { get; }
    string ErrorCode { get; }
}
