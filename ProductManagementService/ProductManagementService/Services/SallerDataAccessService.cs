using MongoDB.Driver;
using ProductManagementService.Models;

namespace ProductManagementService.Services;

public class SallerDataAccessService : IDataAccessService<SallersDetail>
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;
    private readonly string databaseName;
    private readonly IMongoCollection<SallersDetail> _saller;

    public SallerDataAccessService(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
        databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");

        // connect to mongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _saller = database.GetCollection<SallersDetail>("sallers_detail");

    }
    public Task AddData(SallersDetail data)
    {
        return _saller.InsertOneAsync(data);
    }

    public Task DeleteData(string id)
    {
        return _saller.DeleteOneAsync(id);
    }

    public Task<List<SallersDetail>> GetAllData()
    {
        return _saller.Find(_=> true).ToListAsync();
    }

    public Task<SallersDetail> GetDataByID(string id)
    {
        return _saller.Find(x=> x.Id == id).FirstOrDefaultAsync();
    }

    public Task UpdateData(SallersDetail data, string id)
    {
        return _saller.ReplaceOneAsync(x => x.Id == id, data);
    }
}
