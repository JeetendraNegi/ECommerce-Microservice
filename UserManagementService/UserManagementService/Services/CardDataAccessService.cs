using MongoDB.Driver;
using UserManagementService.Models;

namespace UserManagementService.Services;

public class CardDataAccessService : IDataAccessService<CardDetails>
{
    private readonly IConfiguration configuration;
    private readonly string ConnectionString = null;
    private readonly string DatabaseName = null;
    private readonly IMongoCollection<CardDetails> _data;
    public CardDataAccessService(IConfiguration iconfig)
    {
        configuration = iconfig;
        ConnectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
        DatabaseName = configuration.GetValue<string>("MongoDB:DatabaseName");

        //MongoDB connection

        var mongoClient = new MongoClient(ConnectionString);
        var database = mongoClient.GetDatabase(DatabaseName);
        _data = database.GetCollection<CardDetails>("card_detail");
    }

    public Task AddData(CardDetails data)
    {
        return _data.InsertOneAsync(data);
    }

    public Task DeleteData(string id)
    {
        return _data.DeleteOneAsync(id);
    }

    public Task<List<CardDetails>> GetAllData()
    {
        var cardDetails = _data.Find(_ => true).ToListAsync();
        return cardDetails;

    }

    public Task<CardDetails> GetDataByID(string id)
    {
        return _data.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<CardDetails>> GetDataByUserID(string id)
    {
        return _data.Find(x => x.UserID == id).ToListAsync();
    }

    public Task UpdateData(CardDetails data, string id)
    {
        return _data.ReplaceOneAsync(x => x.Id == id, data);
    }
}
