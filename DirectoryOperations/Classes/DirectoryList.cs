using DirectoryOperations.Abstractions;
using Models.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace DirectoryOperations.Classes
{
    /// <summary>
    /// Class to handle listing of directory contents
    /// </summary>
    public class DirectoryList : IDirectoryList
    {
        public DirectoryList(IFileSystem fileSystem,
                             IDirectoryModel directoryModel,
                             ILogger logger, 
                             IException exception)
        {
            _fileSystem = fileSystem;
            _directoryModel = directoryModel;
            _logger = logger;
            _exception = exception;            
        }

        IFileSystem _fileSystem;
        IDirectoryModel _directoryModel;
        Abstractions.ILogger _logger;
        IException _exception;        

        /// <summary>
        /// Get contents of directory as IDirectoryModel
        /// </summary>
        /// <param name="pathToDirectory">Path of directory to get</param>
        /// <returns>Instance implementing IDirectoryModel</returns>
        public IDirectoryModel GetDirectory(string pathToDirectory)
        {
            try
            {
                DirectoryInfoBase directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(pathToDirectory);

                _directoryModel.DateCreated = directoryInfo.CreationTime;
                _directoryModel.LastModified = directoryInfo.LastWriteTime;
                _directoryModel.SubDirectories = directoryInfo.EnumerateDirectories().ToArray<DirectoryInfoBase>();
                _directoryModel.SubDirectoriesList = new List<DirectoryInfoBase>(directoryInfo.GetDirectories());
                _directoryModel.Files = directoryInfo.EnumerateFiles().ToArray<FileInfoBase>();
                _directoryModel.FilesList = new List<FileInfoBase>(directoryInfo.GetFiles());
                _directoryModel.DirectoryPath = pathToDirectory;

                return _directoryModel;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Log($"No directory found at: { pathToDirectory }");
                _logger.Log($"ERROR GETTING DIRECTORY: { ex.Message }");

                throw ex;
            }
            catch (Exception ex)
            {
                _logger.Log($"ERROR GETTING DIRECTORY: { ex.Message }");

                throw ex;
            }
        }

        /// <summary>
        /// Gets subdirectories and file contents of directory
        ///   Uses string[] for less overhead
        /// </summary>
        /// <param name="pathToDirectory">Path of directory's info to lsit</param>
        /// <param name="searchPattern">Search pattern to use when listing contents</param>
        /// <param name="searchOption">Defaults to AllDirectories; Can also send TopDirectoryOnly</param>
        /// <returns>Array of strings of subdirectory and file paths</returns>
        public string[] GetSubDirectoryAndFilePaths(string pathToDirectory,
                                                    string searchPattern = "*",
                                                    SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                string[] directories = 
                    _fileSystem.Directory.GetDirectories(pathToDirectory, searchPattern);
                string[] files = 
                    _fileSystem.Directory.GetFiles(pathToDirectory, searchPattern);

                return directories.Concat(files).ToArray();
            }
            catch (UnauthorizedAccessException ex)
            {
                _exception.HandleException($"Unauthorized Access Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
            catch (Exception ex)
            {
                _exception.HandleException($"Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
        }

        /// <summary>
        /// Gets subdirectories and file contents of directory
        ///   Uses List<string> for additional functionality if needed</string>
        /// </summary>
        /// <param name="pathToDirectory">Path of directory's info to lsit</param>
        /// <param name="searchPattern">Search pattern to use when listing contents</param>
        /// <param name="searchOption">Defaults to AllDirectories; Can also send TopDirectoryOnly</param>
        /// <returns>List of strings of subdirectory and file paths</returns>
        public List<string> GetSubDirectoryAndFilePathsAsList(string pathToDirectory,
                                                              string searchPattern = "*",
                                                              SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                return new List<string>(this.GetSubDirectoryAndFilePaths(pathToDirectory, searchPattern, searchOption));
            }
            catch (UnauthorizedAccessException ex)
            {
                _exception.HandleException($"Unauthorized Access Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
            catch (Exception ex)
            {
                _exception.HandleException($"Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
        }

        /// <summary>
        /// Gets directory's subdirectory paths as array of string
        /// </summary>
        /// <param name="pathToDirectory">Path of directory</param>
        /// <param name="searchPattern">Search pattern to use when listing contents</param>
        /// <param name="searchOption">Defaults to AllDirectories; Can also send TopDirectoryOnly</param>
        /// <returns>Array of string containing subdirectory paths</returns>
        public string[] GetSubDirectoryPaths(string pathToDirectory,
                                             string searchPattern = "*",
                                             SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                return _fileSystem.Directory.GetDirectories(pathToDirectory, searchPattern);                
            }
            catch (UnauthorizedAccessException ex)
            {
                _exception.HandleException($"Unauthorized Access Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
            catch (Exception ex)
            {
                _exception.HandleException($"Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
        }

        /// <summary>
        /// Gets file paths contained in directory as list of string
        /// </summary>
        /// <param name="pathToDirectory">Path of directory's info to lsit</param>
        /// <param name="searchPattern">Search pattern to use when listing contents</param>
        /// <param name="searchOption">Defaults to AllDirectories; Can also send TopDirectoryOnly</param>
        /// <returns>List of string containing file paths in directory matching search pattern</returns>
        public List<string> GetSubDirectoryPathsAsList(string pathToDirectory,
                                                       string searchPattern = "*",
                                                       SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                return new List<string>(_fileSystem.Directory.GetDirectories(pathToDirectory, searchPattern));
            }
            catch (UnauthorizedAccessException ex)
            {
                _exception.HandleException($"Unauthorized Access Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
            catch (Exception ex)
            {
                _exception.HandleException($"Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
        }

        /// <summary>
        /// Gets file paths of files in directory as array of string
        /// </summary>
        /// <param name="pathToDirectory">Path of directory's info to lsit</param>
        /// <param name="searchPattern">Search pattern to use when listing contents</param>
        /// <param name="searchOption">Defaults to AllDirectories; Can also send TopDirectoryOnly</param>
        /// <returns>File paths in directory as array of string</returns>
        public string[] GetDirectoryFilePaths(string pathToDirectory,
                                              string searchPattern = "*",
                                              SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                return _fileSystem.Directory.GetFiles(pathToDirectory, searchPattern);
            }
            catch (UnauthorizedAccessException ex)
            {
                _exception.HandleException($"Unauthorized Access Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
            catch (Exception ex)
            {
                _exception.HandleException($"Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
        }

        /// <summary>
        /// Gets file paths of files in directory as list of string 
        /// </summary>
        /// <param name="pathToDirectory">Path of directory's info to lsit</param>
        /// <param name="searchPattern">Search pattern to use when listing contents</param>
        /// <param name="searchOption">Defaults to AllDirectories; Can also send TopDirectoryOnly</param>
        /// <returns>File paths in directory as list of string</returns>
        public List<string> GetDirectoryFilePathsAsList(string pathToDirectory,
                                                        string searchPattern = "*",
                                                        SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                return new List<string>(_fileSystem.Directory.GetFiles(pathToDirectory, searchPattern));
            }
            catch (UnauthorizedAccessException ex)
            {
                _exception.HandleException($"Unauthorized Access Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
            catch (Exception ex)
            {
                _exception.HandleException($"Exception Listing Contents of { pathToDirectory }");
                throw ex;
            }
        }
    }    
}
