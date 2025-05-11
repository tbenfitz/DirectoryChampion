namespace DirectoryOperations.Abstractions
{
    public interface IFileOperations
    {
        bool MoveFile(string filePath, string moveToPath, bool overwriteExisting);
        bool CopyFile(string filePath, string copyToPath, bool overwriteExisting);
        bool DeleteFile(string filePath);
        bool Exists(string filePath);
    }
}