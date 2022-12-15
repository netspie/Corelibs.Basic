namespace Common.Basic.Files
{
    public interface IDirectoryOperations
    {
        void Create(string path);
        string[] GetFiles(string path);
        bool Exists(string path);
    }
}
