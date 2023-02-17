using MongoDB.Bson.Serialization.Attributes;

namespace OrderManagementService.Models;

public class OrdersDetail
{
    [BsonId]
    public string Id { get; set; }
    public string OrderNo { get; set; }
    public string ProductId { get; set; }
    public string UserId { get; set; }
    public double Price { get; set; }
    public bool OrderStatus { get; set; }
    public string AddressId { get; set; }

}
