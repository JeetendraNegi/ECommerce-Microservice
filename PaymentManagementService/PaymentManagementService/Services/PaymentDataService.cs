using MongoDB.Driver;
using PaymentManagementService.Models;

namespace PaymentManagementService.Services;

public class PaymentDataService : IDataAccessService<PaymentDetails>
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;
    private readonly string databaseName;
    private readonly IMongoCollection<PaymentDetails> _payments;

    public PaymentDataService(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetValue<string>("MongoDB:ConnectionURI");
        databaseName = _configuration.GetValue<string>("MongoDB:DatabaseName");

        // connect to MongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _payments = database.GetCollection<PaymentDetails>("payment_detail");
    }
    public Task AddData(PaymentDetails data)
    {
        return _payments.InsertOneAsync(data);
    }

    public Task DeleteData(string id)
    {
        return _payments.DeleteOneAsync(id);
    }

    public Task<List<PaymentDetails>> GetAllData()
    {
        return _payments.Find(_ => true).ToListAsync();
    }

    public Task<PaymentDetails> GetDataById(string id)
    {
        return _payments.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task UpdateData(PaymentDetails data, string id)
    {
        return _payments.ReplaceOneAsync(x => x.Id == id, data);
    }
}
