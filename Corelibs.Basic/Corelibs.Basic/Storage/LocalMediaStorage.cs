using Corelibs.Basic.Blocks;

namespace Corelibs.Basic.Storage;

public class LocalMediaStorage : IMediaStorage
{
    private readonly string _baseWritePath;
    private readonly string _baseReadPath;

    public LocalMediaStorage(
        string baseWritePath, string baseReadPath)
    {
        _baseWritePath = baseWritePath;
        _baseReadPath = baseReadPath;
    }

    public Task<Result> Get(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Save(Stream stream, string name)
    {
        string filePath = Path.Combine(_baseWritePath, name);

        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        await using FileStream fs = new(filePath, FileMode.Create);
        await stream.CopyToAsync(fs);

        return Result.Success();
    }
}

public class LocalMediaStorage<T> : LocalMediaStorage, IMediaStorage<T>
{
    public LocalMediaStorage(string baseWritePath, string baseReadPath) : base(baseWritePath, baseReadPath)
    {
    }
}
