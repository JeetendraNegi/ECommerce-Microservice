using UserManagementService.Models;
using MongoDB.Driver;

namespace UserManagementService.Services;

public class UserDataAccessService : IDataAccessService<UserDetails>
{
    private readonly IConfiguration configuration;
    private readonly string ConnectionString = null;
    private readonly string DatabaseName = null;
    private readonly IMongoCollection<UserDetails> _users;
    public UserDataAccessService(IConfiguration iconfig)
    {
        configuration = iconfig;
        ConnectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
        DatabaseName = configuration.GetValue<string>("MongoDB:DatabaseName");

        //MongoDB connection

        var mongoClient = new MongoClient(ConnectionString);
        var database = mongoClient.GetDatabase(DatabaseName);
        _users = database.GetCollection<UserDetails>("users_detail");
    }

    public Task AddData(UserDetails data)
    {
        return _users.InsertOneAsync(data);
    }

    public Task DeleteData(string id)
    {
        return _users.DeleteOneAsync(id);
    }

    public Task<List<UserDetails>> GetAllData()
    {
        var users = _users.Find(_ => true).ToListAsync();
        return users;

    }

    public Task<UserDetails> GetDataByID(string id)
    {
        return _users.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<UserDetails>> GetDataByUserID(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateData(UserDetails data, string id)
    {
        return _users.ReplaceOneAsync(x => x.Id == id, data);
    }
}
