using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Common.Types;

public class GamePagination
{
    public ICollection<Game> Games { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}
