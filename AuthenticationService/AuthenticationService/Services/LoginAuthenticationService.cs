using AuthenticationService.Models;
using MongoDB.Driver;

namespace AuthenticationService.Services;

public class LoginAuthenticationService : IAuthenticationService<UserLogin>
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;
    private readonly string databaseName;
    private readonly string collectionName;
    private readonly IMongoCollection<UserLogin> users;

    public LoginAuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetValue<string>("MongoDB:ConnectionURI");
        databaseName = _configuration.GetValue<string>("MongoDB:DatabaseName");
        collectionName = _configuration.GetValue<string>("MongoDB:CollectionName");

        //Mongodb connection
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        users = database.GetCollection<UserLogin>(collectionName);
    }
    public async Task AddData(UserLogin model)
    {
        await users.InsertOneAsync(model);
    }

    public async Task DeleteData(string id)
    {
        await users.DeleteOneAsync(id);
    }

    public async Task<IEnumerable<UserLogin>> GetAllData()
    {
        return await users.Find(_=>true).ToListAsync();
    }

    public async Task<UserLogin> GetDataById(string UserId)
    {
        return await users.Find(x=>x.UserName == UserId).FirstOrDefaultAsync();
    }

    public async Task UpdateData(UserLogin model)
    {
        await users.ReplaceOneAsync(x=>x.Id == model.Id, model);
    }

    public async Task<bool> ValidateData(string userId)
    {
        return await users.Find(x=>x.UserName == userId).AnyAsync();
    }
}
