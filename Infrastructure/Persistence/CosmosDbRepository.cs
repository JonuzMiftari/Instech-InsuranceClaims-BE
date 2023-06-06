// TODO: delete this repo

//using Application.Common.Interfaces;
//using Domain.Common;
//using Domain.Entities;
//using Microsoft.Azure.Cosmos;
//using Newtonsoft.Json;

//namespace Infrastructure.Persistence;
//public class CosmosDbRepository<T> : IRepository<T> where T : BaseEntity<string>
//{
//    private readonly Container _container;

//    public CosmosDbRepository(CosmosClient cosmosClient, string databaseName, string containerId)
//    {
//        _container = cosmosClient?.GetContainer(databaseName, containerId)
//                     ?? throw new ArgumentNullException(nameof(cosmosClient));
//    }

//    public async Task<IEnumerable<T>> GetClaimsAsync()
//    {
//        var query = _container.GetItemQueryIterator<Claim>(new QueryDefinition("SELECT * FROM c"));
//        var results = new List<Claim>();
//        while (query.HasMoreResults)
//        {
//            var response = await query.ReadNextAsync();

//            results.AddRange(response.ToList());
//        }
//        return (IEnumerable<T>) results;
//    }

//    public async Task<T?> GetByIdAsync(string id)
//    {
//        try
//        {
//            var response = await _container.ReadItemAsync<Claim>(id, new PartitionKey(id));
//            return JsonConvert.DeserializeObject<T>(response.Resource.ToString());
//        }
//        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
//        {
//            return null;
//        }
//    }

//    public Task AddAsync(T entity) 
//        => _container.CreateItemAsync(entity, new PartitionKey(entity.Id));

//    public Task DeleteAsync(string id) 
//        => _container.DeleteItemAsync<T>(id, new PartitionKey(id));
//}



using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Persistence;
public abstract class BaseCosmosDbRepository<T> : IRepository<T> where T : BaseEntity<string>
{
    private readonly IClaimsDbContext _claimsDbContext;

    public BaseCosmosDbRepository(IClaimsDbContext claimsDbContext)
    {
        _claimsDbContext = claimsDbContext;
    }


    public async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        return await _claimsDbContext.Claims.ToListAsync();
    }

    public Task<Claim> GetByIdAsync(string id)
    {
        return _claimsDbContext.Claims.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Claim entity)
    {
        await _claimsDbContext.Claims.AddAsync(entity);
        await _claimsDbContext.SaveChangesAsync(new CancellationToken());
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        _claimsDbContext.Claims.Remove(entity);
        await _claimsDbContext.SaveChangesAsync(new CancellationToken());
    }
}
