using System.Collections.Generic;

namespace AuthenticationService.Services;

public interface IAuthenticationService<T> where T : class
{
    public Task<IEnumerable<T>> GetAllData();

    public Task<T> GetDataById(string id);

    public Task AddData(T model);

    public Task UpdateData(T model);

    public Task DeleteData(string id);

    public Task<bool> ValidateData(string id);
}
