using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserManagementService.Models;

public class UserDetails
{
    [BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNo { get; set; }
    public List<AddressDetails> UserAddress { get; set; }
    public List<CardDetails> CardDetail { get; set; }
}
