using OrdersWorker.Application.Orders.Abstractions;
using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders.Executors;

public class NotificationExecutor : IOrderExecutor
{
    public async Task ProcessOrder(Order order)
    {
        await Task.Delay(1000);
    }
}