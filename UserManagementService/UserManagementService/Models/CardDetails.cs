using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserManagementService.Models;

public class CardDetails
{
    [BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserID { get; set; }
    public string CardType { get; set; }
    public string CardNo { get; set; }
    public string ExpDate { get; set; }
}
