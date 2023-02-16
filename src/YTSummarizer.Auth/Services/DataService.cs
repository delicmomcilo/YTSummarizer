using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YTSummarizer.Auth.Models;
using YTSummarizer.Models;

namespace YTSummarizer.Auth.Services;

public class DataService : IDataService
{
    private readonly IMongoCollection<Data> _dataCollection;

    public DataService(
        IOptions<MongoDBSettings> dataStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            dataStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dataStoreDatabaseSettings.Value.DatabaseName);

        _dataCollection = mongoDatabase.GetCollection<Data>(
            dataStoreDatabaseSettings.Value.DataCollection);
    }

    public async Task<List<Data>> GetAsync() =>
        await _dataCollection.Find(_ => true).ToListAsync();

    public async Task<Data?> GetAsync(string id) =>
        await _dataCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Data>> GetByUserIdAsync(string id) =>
            await _dataCollection.Find(x => x.CreatedBy == id).ToListAsync();
    public async Task CreateAsync(Data data)
    {
        await _dataCollection.InsertOneAsync(data);
    }

    public async Task UpdateAsync(string id, Data data) =>
        await _dataCollection.ReplaceOneAsync(x => x.Id == id, data);

    public async Task RemoveAsync(string id) =>
        await _dataCollection.DeleteOneAsync(x => x.Id == id);
}