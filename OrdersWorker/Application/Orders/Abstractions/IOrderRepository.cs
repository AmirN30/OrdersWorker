using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders.Abstractions;

public interface IOrderRepository
{
    Task<Order?> GetNextOrder();
    Task MarkOrderAsProcessed(Order order);
}