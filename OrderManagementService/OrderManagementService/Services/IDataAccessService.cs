namespace OrderManagementService.Services;

public interface IDataAccessService<T> where T : class
{
    public Task<List<T>> GetAllData();
    public Task<T> GetDataById(string id);
    public Task AddData(T data);
    public Task DeleteData(string id);
    public Task UpdateData(T data, string id);
}
