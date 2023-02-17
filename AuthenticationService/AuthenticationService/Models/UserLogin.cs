using MongoDB.Bson.Serialization.Attributes;

namespace AuthenticationService.Models;

public class UserLogin
{
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; }= string.Empty;
    public IEnumerable<string> Roles { get; set; }
    public bool IsActive { get; set; } = false;
}
