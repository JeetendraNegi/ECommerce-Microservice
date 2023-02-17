using MongoDB.Driver;
using OrderManagementService.Models;

namespace OrderManagementService.Services;

public class OrderDataAccessService : IDataAccessService<OrdersDetail>
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;
    private readonly string databaseName;
    private readonly IMongoCollection<OrdersDetail> _order;

    public OrderDataAccessService(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetValue<string>("MongoDB:ConnectionURI");
        databaseName = _configuration.GetValue<string>("MongoDB:DatabaseName");

        //connect to mongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _order = database.GetCollection<OrdersDetail>("orders_detail");
    }
    public Task AddData(OrdersDetail data)
    {
        return _order.InsertOneAsync(data);
    }

    public Task DeleteData(string id)
    {
        return _order.DeleteOneAsync(id);
    }

    public Task<List<OrdersDetail>> GetAllData()
    {
        return _order.Find(_ => true).ToListAsync();
    }

    public Task<OrdersDetail> GetDataById(string id)
    {
        return _order.Find(x=> x.Id == id).FirstOrDefaultAsync();
    }

    public Task UpdateData(OrdersDetail data, string id)
    {
        return _order.ReplaceOneAsync(x=> x.Id == id, data);
    }
}
