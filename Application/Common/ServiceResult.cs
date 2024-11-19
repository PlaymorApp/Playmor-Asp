namespace Playmor_Asp.Application.Common;

public class ServiceResult<T, TError>
{
    public List<TError> Errors = [];
    public bool IsValid => Errors.Count == 0;
    public required T Data { get; set; }
}
