using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.Repository;
using System.Net.Http.Json;

namespace BuildingBlocks.Repository;

public class WWWRootHttpClientRepository<T> : IRepository<T>
{
    private readonly HttpClient _httpClient;
    private readonly string _directory;
    private readonly string _version;

    public WWWRootHttpClientRepository(HttpClient httpClient, string directory, string version)
    {
        _httpClient = httpClient;
        _directory = directory;
        _version = version;
    }

    public Task<Result> Clear()
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> ExistsOfName(string name, Func<T, string> getName)
    {
        throw new NotImplementedException();
    }

    public Task<Result<T[]>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<T[]>> GetAll(Action<int> setProgress, CancellationToken ct)
    {
        string path = GetResourcePath("items.dat");
        var item = await _httpClient.GetStringAsync(path);
        var itemsNames = item
            .Split('\n')
            .Where(i => !i.IsNullOrEmpty())
            .ToArray();

        var itemsResults = await Task.WhenAll(itemsNames.Select(GetBy));
        var items = itemsResults.Select(i => i.Get()).ToArray();

        return Result<T[]>.Success(items);
    }

    public async Task<Result<T>> GetBy(string id)
    {
        if (string.IsNullOrEmpty(id))
            return default(T).ToResult();

        string path = GetJsonResourcePath(id);
        var item = await _httpClient.GetFromJsonAsync<T>(path, new System.Text.Json.JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });

        return item.ToResult();
    }

    public async Task<Result<T[]>> GetBy(IList<string> ids)
    {
        var results = await Task.WhenAll(ids.Select(id => GetBy(id)));
        var items = results.Select(i => i.Get()).ToArray();

        return Result<T[]>.Success(items).With(results);
    }

    public Task<Result<T>> GetOfName(string name, Func<T, string> getName)
    {
        throw new NotImplementedException();
    }

    public Task Save(string id, T item)
    {
        string path = GetJsonResourcePath(id);
        return _httpClient.PostAsJsonAsync(path, item, new System.Text.Json.JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });
    }

    public Task<Result> Save(T item)
    {
        throw new NotImplementedException();
    }

    private string GetJsonResourcePath(string id) =>
        $"{_directory}/{id}.json?v={_version}";


    private string GetResourcePath(string name) =>
        $"{_directory}/{name}";
}