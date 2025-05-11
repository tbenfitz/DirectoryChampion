using Models.Abstractions;
using System.Collections.Generic;
using System.IO;

namespace DirectoryOperations.Abstractions
{
    public interface IDirectoryList
    {
        IDirectoryModel GetDirectory(string pathToDirectory);

        string[] GetSubDirectoryAndFilePaths(string pathToDirectory, 
                                             string searchPattern = "*",
                                             SearchOption searchOption = SearchOption.AllDirectories);

        List<string> GetSubDirectoryAndFilePathsAsList(string pathToDirectory, 
                                                       string searchPattern = "*",
                                                       SearchOption searchOption = SearchOption.AllDirectories);

        string[] GetSubDirectoryPaths(string pathToDirectory,
                                             string searchPattern = "*",
                                             SearchOption searchOption = SearchOption.AllDirectories);

        List<string> GetSubDirectoryPathsAsList(string pathToDirectory,
                                                string searchPattern = "*",
                                                SearchOption searchOption = SearchOption.AllDirectories);

        string[] GetDirectoryFilePaths(string pathToDirectory,
                                        string searchPattern = "*",
                                        SearchOption searchOption = SearchOption.AllDirectories);

        List<string> GetDirectoryFilePathsAsList(string pathToDirectory,
                                                 string searchPattern = "*",
                                                 SearchOption searchOption = SearchOption.AllDirectories);
    }
}