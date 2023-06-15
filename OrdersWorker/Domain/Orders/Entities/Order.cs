using System.Runtime.Serialization;

namespace OrdersWorker.Domain.Orders.Entities;

public class Order
{
    public long Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
}

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending = 0,
    [EnumMember(Value = "InProgress")]
    InProgress = 1,
    [EnumMember(Value = "Completed")]
    Completed = 2
}