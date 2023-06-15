using OrdersWorker.Application.Orders.Abstractions;
using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders;

public class OrderPipeline : IOrderPipeline
{
    private readonly IOrderExecutor[] _orderProcessors;

    public OrderPipeline(IOrderExecutor[] orderProcessors)
    {
        _orderProcessors = orderProcessors;
    }

    public async Task Execute(Order order)
    {
        var processorTasks = _orderProcessors.Select(processor => processor.ProcessOrder(order));
        await Task.WhenAll(processorTasks);
    }
}