using OrdersWorker.Application.Orders.Abstractions;
using OrdersWorker.Application.Orders.Executors;
using OrdersWorker.Application.Orders.Processors;
using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Application.Orders;

public class OrderExecutorsPipelineBuilder
{
    public IReadOnlyList<IOrderExecutor> Build(Order order)
    {
        var pipeline = new List<IOrderExecutor>();
        
        switch (order.Status)
        {
            case OrderStatus.Pending:
                break;
            case OrderStatus.InProgress:
                pipeline.AddRange(new List<IOrderExecutor>
                {
                    new WarehouseReservationExecutor(),
                    new NotificationExecutor()
                });
                break;
            case OrderStatus.Completed:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return pipeline;
    }
}