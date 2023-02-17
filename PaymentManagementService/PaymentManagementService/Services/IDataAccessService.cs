namespace PaymentManagementService.Services;

public interface IDataAccessService<T> where T : class
{
    public Task<List<T>> GetAllData();
    public Task<T> GetDataById(string id);
    public Task AddData(T data);
    public Task UpdateData(T data, string id);
    public Task DeleteData(string id);

}
