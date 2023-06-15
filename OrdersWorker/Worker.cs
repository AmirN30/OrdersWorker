using OrdersWorker.Application.Orders.Abstractions;

namespace OrdersWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOrderService _orderService;

    public Worker(ILogger<Worker> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _orderService.ExecutePendingOrder();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}