using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using OrdersWorker.Application.Orders.Abstractions;
using OrdersWorker.Domain.Orders.Entities;

namespace OrdersWorker.Infrastructure.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly string? _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("ConnectionStrings:OrdersDB");
    }
    
    public async Task<Order?> GetNextOrder()
    {
        var sql = @"UPDATE TOP (1) Orders WITH (UPDLOCK, READPAST)
                    SET Status = 'InProgress'
                    OUTPUT inserted.*
                    WHERE Status = 'Pending'
                    ORDER BY CreatedDate ASC;";

        await using var connection = new SqlConnection(_connectionString);
        
        return await connection.QueryFirstOrDefaultAsync<Order>(
            sql,
            commandType: CommandType.Text
        );
    }

    public async Task MarkOrderAsProcessed(Order order)
    {
        var sql = @"UPDATE Orders
                    SET Status = 'Processed'
                    WHERE Id = @Id";

        await using var connection = new SqlConnection(_connectionString);
        
        await connection.ExecuteAsync(
            sql,
            new
            {
                Id = order.Id
            },
            commandType: CommandType.Text
        );
    }
}