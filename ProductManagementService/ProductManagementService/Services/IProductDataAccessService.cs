namespace ProductManagementService.Services;

public interface IProductDataAccessService<T> : IDataAccessService<T> where T : class
{
    public Task<List<T>> GetProductBySallerID(string sallerId);
}
