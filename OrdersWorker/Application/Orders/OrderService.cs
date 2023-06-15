using OrdersWorker.Application.Orders.Abstractions;

namespace OrdersWorker.Application.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderExecutorsPipelineBuilder _orderExecutorsPipelineBuilder;
    
    public OrderService(IOrderRepository orderRepository, OrderExecutorsPipelineBuilder orderExecutorsPipelineBuilder)
    {
        _orderRepository = orderRepository;
        _orderExecutorsPipelineBuilder = orderExecutorsPipelineBuilder;
    }

    public async Task ExecutePendingOrder()
    {
        try
        {
            //Для атомарно конкурентного доступа к заказу тут использовал блокировку в бд
            //Возможен другой подход с использованием Redis для распределенной блокировки
            var orderInProgress = await _orderRepository.GetNextOrder();

            if (orderInProgress is null) return;

            var orderExecutionPipelineItems = _orderExecutorsPipelineBuilder.Build(orderInProgress);

            var orderExecutorPipeline = new OrderPipeline(orderExecutionPipelineItems.ToArray());

            await orderExecutorPipeline.Execute(orderInProgress);

            await _orderRepository.MarkOrderAsProcessed(orderInProgress);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}