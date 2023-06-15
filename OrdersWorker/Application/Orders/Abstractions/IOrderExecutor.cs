using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders.Abstractions;

public interface IOrderExecutor
{
    Task ProcessOrder(Order order);
}