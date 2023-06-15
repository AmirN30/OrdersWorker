using OrdersWorker;
using OrdersWorker.Application.Orders;
using OrdersWorker.Application.Orders.Abstractions;
using OrdersWorker.Infrastructure.Orders;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IOrderService, OrderService>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<OrderExecutorsPipelineBuilder>();
        
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();