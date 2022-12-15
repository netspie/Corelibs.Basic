using System.IO;

namespace Common.Basic.Files
{
    public class FileOperations : IFileOperations
    {
        void IFileOperations.Delete(string filePath) => File.Delete(filePath);
        string IFileOperations.ReadAsText(string filePath) => File.ReadAllText(filePath);
        void IFileOperations.WriteAsText(string filePath, string text) => File.WriteAllText(filePath, text);
    }
}
