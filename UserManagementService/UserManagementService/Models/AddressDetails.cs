using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserManagementService.Models;

public class AddressDetails
{
    [BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserID { get; set; }
    public string AddressType { get; set; }
    public string HouseNo { get; set; }
    public string StreetName { get; set; }
    public string NearBy { get; set; }
    public string AreaPIN { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}
