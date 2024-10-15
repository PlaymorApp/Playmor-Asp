namespace Playmor_Asp.Application.Common;

public class ServiceResult<T, TError>
{
    public List<TError> Errors = [];
    public List<string> Warnings = [];
    public bool IsValid => Errors.Count == 0;
    public bool HasWarnings => Warnings.Count != 0;
    public required T Data { get; set; }
}
