using CaffeTipping.DbServices.Dtos;
using CaffeTipping.DbServices.Services;

namespace CaffeTiping.WebApp;

public class StatisticsComputer(
    ILogger<StatisticsComputer> logger,
    IStatisticsService statisticsService,
    IOrderTipService tipService) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(15);
    private static readonly PeriodicTimer Timer = new(Interval);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogTrace("Starting statistics computer");

        while (await Timer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogTrace("Updating statistics");
            var orders = await tipService.GetAllOrderTips();
            var stats = await statisticsService.GetLatestAsync();

            stats.TotalTips = orders.Select(o => o.Tip).Sum();
            stats.AverageRating = CountAverageRating(orders);
            stats.AverageTipPerc = CountTipPercAverage(orders);
            stats.HighestTipOfDay = FindBiggestTipOfTheDay(orders);

            await statisticsService.UpdateStatistics(stats);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogTrace("Stopping statistics computer");
        Timer.Dispose();
        await base.StopAsync(cancellationToken);
    }

    private double CountAverageRating(List<OrderTipDto> orders)
    {
        try
        {
            return orders.Where(o => o.Rating >= 0).Average(o => o.Rating);
        }
        catch (Exception)
        {
            return 5;
        }
    }


    private double CountTipPercAverage(List<OrderTipDto> orders)
    {
        try
        {
            return orders.Select(o => o.Tip * 100 / o.Bill).Average();
        }
        catch (Exception)
        {
            return 0;
        }
    }


    private Guid? FindBiggestTipOfTheDay(List<OrderTipDto> orders)
    {
        try
        {
            return orders
                .Where(o => o.Date.DayOfYear.Equals(DateTime.Today.DayOfYear) && o.Date.Year.Equals(DateTime.Today.Year))
                .MaxBy(o => o.Tip)?.Id;
        }
        catch (Exception)
        {
            return null;
        }
    }
}