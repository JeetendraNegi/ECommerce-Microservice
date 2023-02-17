using MongoDB.Driver;
using UserManagementService.Models;

namespace UserManagementService.Services;

public class AddressDataAccessService : IDataAccessService<AddressDetails>
{
    private readonly IConfiguration configuration;
    private readonly string ConnectionString = null;
    private readonly string DatabaseName = null;
    private readonly IMongoCollection<AddressDetails> _data;
    public AddressDataAccessService(IConfiguration iconfig)
    {
        configuration = iconfig;
        ConnectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
        DatabaseName = configuration.GetValue<string>("MongoDB:DatabaseName");

        //MongoDB connection

        var mongoClient = new MongoClient(ConnectionString);
        var database = mongoClient.GetDatabase(DatabaseName);
        _data = database.GetCollection<AddressDetails>("address_detail");
    }

    public Task AddData(AddressDetails data)
    {
        return _data.InsertOneAsync(data);
    }

    public Task DeleteData(string id)
    {
        return _data.DeleteOneAsync(id);
    }

    public Task<List<AddressDetails>> GetAllData()
    {
        var addressDetails = _data.Find(_ => true).ToListAsync();
        return addressDetails;

    }

    public Task<AddressDetails> GetDataByID(string id)
    {
        return _data.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<AddressDetails>> GetDataByUserID(string id)
    {
        return _data.Find(x => x.UserID == id).ToListAsync();
    }

    public Task UpdateData(AddressDetails data, string id)
    {
        return _data.ReplaceOneAsync(x => x.Id == id, data);
    }
}
