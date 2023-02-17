using MongoDB.Bson.Serialization.Attributes;

namespace PaymentManagementService.Models;

public class PaymentDetails
{
    [BsonId]
    public string Id { get; set; }
    public string PaymentMethod { get; set; }
    public string OrderId { get; set; }
    public string CardId { get; set; }
    public bool Status { get; set; }
}
