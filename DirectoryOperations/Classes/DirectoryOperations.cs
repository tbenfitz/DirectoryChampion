using DirectoryOperations.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace DirectoryOperations.Classes
{
    /// <summary>
    /// Class to handle CRUD directory operations
    /// </summary>
    public class DirectoryOperations : IDirectoryOperations
    {
        public DirectoryOperations(IFileSystem fileSystem,
                                   ILogger logger, 
                                   IException exception)
        {
            _fileSystem = fileSystem;
            _logger = logger;
            _exception = exception;
        }

        IFileSystem _fileSystem;
        ILogger _logger;
        IException _exception;

        /// <summary>
        /// Attempts to create directory at specified path
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <returns>true if succeeds; false if fails</returns>
        public bool CreateDirectory(string path)
        {
            try
            {
                _fileSystem.Directory.CreateDirectory(path);
                return true;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log($"Path Too Long Exception Creating: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"Unauthorized Exception Creating: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Log($"Directory Not Found Exception Creating: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (IOException ex)
            {
                _logger.Log($"IO Exception Creating: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }            
            catch (Exception ex)
            {
                _logger.Log($"Unexpected  Exception Creating { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
        }

        /// <summary>
        /// Attempts to move a directory
        /// </summary>
        /// <param name="sourcePath">Soure path of directory to move</param>
        /// <param name="destinationPath">Desitination path of directory</param>
        /// <returns>true if succeeds; false if fails</returns>
        public bool MoveDirectory(string sourcePath, string destinationPath)
        {
            try
            {
                _fileSystem.Directory.Move(sourcePath, destinationPath);                
                return true;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log($"Path Too Long Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"Unauthorized Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Log($"Directory Not Found Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (IOException ex)
            {
                _logger.Log($"IO Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log($"Unexpected  Exception Moving { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
        }

        /// <summary>
        /// Attempts to copy a directory from a source to a destination
        ///   Will not overwrite existing files
        /// </summary>
        /// <param name="sourcePath">Source path of directory to copy</param>
        /// <param name="destinationPath">Destination path to copy to</param>
        /// <returns>true if success; false if not</returns>
        public bool CopyDirectory(string sourcePath, string destinationPath)
        {
            try
            {
                if (!_fileSystem.Directory.Exists(destinationPath))
                {
                    _fileSystem.Directory.CreateDirectory(destinationPath);
                }

                string[] files = _fileSystem.Directory.GetFiles(sourcePath);

                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destinationPath, name);
                    _fileSystem.File.Copy(file, dest);
                }
                string[] folders = _fileSystem.Directory.GetDirectories(sourcePath);

                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destinationPath, name);
                    CopyDirectory(sourcePath, destinationPath);
                }

                return true;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log($"Path Too Long Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"Unauthorized Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Log($"Directory Not Found Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (IOException ex)
            {
                _logger.Log($"IO Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log($"Unexpected  Exception Moving { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
        }

        /// <summary>
        /// Deletes a directory if it is not empty
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <returns>True is succeeds; False if not</returns>
        public bool DeleteDirectory(string path)
        {
            try
            {
                if (_fileSystem.Directory.Exists(path))
                {
                    _fileSystem.Directory.Delete(path);
                    _logger.Log($"{ path } has been deleted");
                }
                else
                {
                    _logger.Log($"{ path } cannot be deleted as it does not exist");
                    return false;
                }

                return true;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log($"Path Too Long Exception deleting: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"Unauthorized Exception deleting: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (IOException ex)
            {
                _logger.Log($"IO Exception deleting: { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log($"Unexpected Exception deleting { path }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
        }

        /// <summary>
        /// Determines if directory exists
        /// </summary>
        /// <param name="path">Path of directory to check</param>
        /// <returns>true if succeeds; false if fails</returns>
        public bool Exists(string path)
        {
            return _fileSystem.Directory.Exists(path);
        }  
    }
}
