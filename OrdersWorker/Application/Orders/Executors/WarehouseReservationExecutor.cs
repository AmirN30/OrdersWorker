using OrdersWorker.Application.Orders.Abstractions;
using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders.Processors;

public class WarehouseReservationExecutor : IOrderExecutor
{
    public async Task ProcessOrder(Order order)
    {
        await Task.Delay(1000);
    }
}