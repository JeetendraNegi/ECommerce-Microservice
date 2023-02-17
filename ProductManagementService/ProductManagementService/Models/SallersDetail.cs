using MongoDB.Bson.Serialization.Attributes;

namespace ProductManagementService.Models;

public class SallersDetail
{
    [BsonId]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNo { get; set; }
    public string Address { get; set; }
    public List<ProductDetail> Products { get; set; }

}
