namespace DirectoryOperations.Abstractions
{
    public interface IDirectoryOperations
    {
        bool CopyDirectory(string sourcePath, string destinationPath);
        bool CreateDirectory(string path);
        bool DeleteDirectory(string path);
        bool MoveDirectory(string sourcePath, string destinationPath);
        bool Exists(string path);
    }
}