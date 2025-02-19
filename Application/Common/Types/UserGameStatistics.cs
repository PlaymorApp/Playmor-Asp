namespace Playmor_Asp.Application.Common.Types;

public class UserGameStatistics
{
    public int Games { get; set; }
    public int GamesCompleted { get; set; }
    public int GamesInProgress { get; set; }
    public int GamesDropped { get; set; }
    public int GamesPlanned { get; set; }
    public double AverageRating { get; set; }
}
