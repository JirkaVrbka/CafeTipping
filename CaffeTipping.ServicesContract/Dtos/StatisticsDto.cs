namespace CaffeTipping.ServicesContract.Dtos;

public class StatisticsDto
{
    public Guid Id { get; set; }
    public DateOnly Inserted { get; set; }
    public DateTime Updated { get; set; }
    public double TotalTips { get; set; }
    public Guid? HighestTipOfDay { get; set; }
    public double AverageTipPerc { get; set; }
    public double AverageRating { get; set; }
}