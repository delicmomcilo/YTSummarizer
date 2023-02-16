using YTSummarizer.Auth.Models;

namespace YTSummarizer.Auth.Services;
public interface IDataService
{
    public Task<List<Data>> GetAsync();

    public Task<Data?> GetAsync(string id);

    public Task<List<Data>> GetByUserIdAsync(string id);
    public Task CreateAsync(Data data);

    public Task UpdateAsync(string id, Data updatedData);

    public Task RemoveAsync(string id);
}