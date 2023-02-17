using MongoDB.Bson.Serialization.Attributes;

namespace ProductManagementService.Models;

public class ProductDetail
{
    [BsonId]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }
    public string SallerId { get; set; }
    public string ImageURI { get; set; }
}
