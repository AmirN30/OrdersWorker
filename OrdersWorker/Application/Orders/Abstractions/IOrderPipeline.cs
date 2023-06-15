using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders.Abstractions;

public interface IOrderPipeline
{
    Task Execute(Order order);
}