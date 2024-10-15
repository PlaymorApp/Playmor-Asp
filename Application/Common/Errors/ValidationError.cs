namespace Playmor_Asp.Application.Common.Errors;

public class ValidationError : IError
{
    public string Message { get; }
    public string FieldName { get; }
    public string ErrorCode { get; }

    public ValidationError(string fieldName, string message)
    {
        FieldName = fieldName;
        Message = message;
        ErrorCode = "VALIDATION_ERROR";
    }
}
