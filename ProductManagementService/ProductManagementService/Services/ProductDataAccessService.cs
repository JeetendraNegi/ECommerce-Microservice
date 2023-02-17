using MongoDB.Driver;
using ProductManagementService.Models;

namespace ProductManagementService.Services;

public class ProductDataAccessService : IProductDataAccessService<ProductDetail>
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;
    private readonly string databaseName;
    private readonly IMongoCollection<ProductDetail> _products;

    public ProductDataAccessService(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = configuration.GetValue<string>("MongoDB:ConnectionURI");
        databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");

        //Connect to mongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _products = database.GetCollection<ProductDetail>("products_detail");
    }
    public Task AddData(ProductDetail data)
    {
        return _products.InsertOneAsync(data);  
    }

    public Task DeleteData(string id)
    {
        return _products.DeleteOneAsync(id);
    }

    public Task<List<ProductDetail>> GetAllData()
    {
        return _products.Find(_=> true).ToListAsync();
    }

    public Task<ProductDetail> GetDataByID(string id)
    {
        return _products.Find(x=>x.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<ProductDetail>> GetProductBySallerID(string sallerId)
    {
        return _products.Find(x => x.SallerId == sallerId).ToListAsync();
    }

    public Task UpdateData(ProductDetail data, string id)
    {
        return _products.ReplaceOneAsync(x => x.Id == id, data);
    }
}
