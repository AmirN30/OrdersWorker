namespace OrdersWorker.Application.Orders.Abstractions;

public interface IOrderService
{
    Task ExecutePendingOrder();
}