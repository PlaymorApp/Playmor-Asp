using Playmor_Asp.Application.Common.Types;

namespace Playmor_Asp.Application.Common.Filters;

public class GameFilter
{
    public DateRange? DateRange { get; set; }
    public ICollection<string>? Genres { get; set; }
    public ICollection<string>? Modes { get; set; }
    public ICollection<string>? Developers { get; set; }
    public ICollection<string>? Publishers { get; set; }
    public ICollection<string>? Platforms { get; set; }
}
