using DirectoryOperations.Abstractions;
using DirectoryOperations.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace DirectoryOperations.Classes
{
    /// <summary>
    /// Class for CRUD file operations
    /// </summary>
    public class FileOperations : IFileOperations
    {
        public FileOperations(IFileSystem fileSystem,
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
        /// Attempts to move a file from a source path to a destination path
        /// If file exists in the destination
        /// </summary>
        /// <param name="sourcePath">Path of file to move</param>
        /// <param name="destinationPath">Path file is to be moved to</param>
        /// <param name="overwriteIfExists">
        ///     Whether or not to overwrite existing file
        /// </param>
        /// <returns>true if succeeds; false if fails</returns>
        public bool MoveFile(string sourcePath, 
                             string destinationPath,
                             bool overwriteIfExists)
        {
            try
            {
                if (_fileSystem.File.Exists(destinationPath))
                {
                    if (overwriteIfExists)
                    {
                        _fileSystem.File.Delete(destinationPath);
                    }
                    else
                    {
                        _logger.Log($"ERROR MOVING FILE: { destinationPath } exists");
                        return false;
                    }
                }

                _fileSystem.Directory
                    .CreateDirectory(FileSystemUtilities.GetPathToFilesDirectory(destinationPath));                

                _fileSystem.File.Move(sourcePath, destinationPath);

                _logger.Log($"File has been moved to { destinationPath }");

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
            catch (IOException ex)
            {
                _logger.Log($"IO Exception Moving: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }                       
            catch (Exception ex)
            {
                _logger.Log($"Unexpected Exception Moving { sourcePath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }                                  
        }

        /// <summary>
        /// Attempts to copy a file from a source path to a destination path
        /// If file exists in the destination
        /// </summary>
        /// <param name="sourcePath">Path of file to move</param>
        /// <param name="destinationPath">Path file is to be moved to</param>
        /// <param name="overwriteIfExists">
        ///     Whether or not to overwrite existing file
        /// </param>
        /// <returns>true if succeeds; false if fails</returns>
        public bool CopyFile(string sourcePath, 
                             string destinationPath,
                             bool overwriteIfExists)
        {
            try
            {                
                if (_fileSystem.File.Exists(destinationPath))
                {                    
                    if (overwriteIfExists)
                    {
                        _fileSystem.File.Delete(destinationPath);
                    }
                    else
                    {
                        _logger.Log($"ERROR MOVING FILE: { destinationPath } exists");
                        return false;
                    }
                }

                _fileSystem.Directory
                    .CreateDirectory(FileSystemUtilities.GetPathToFilesDirectory(destinationPath));

                _fileSystem.File.Copy(sourcePath, destinationPath);

                _logger.Log($"File has been copied to { destinationPath }");

                return true;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log($"Path Too Long Exception copying: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"Unauthorized Exception copying: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (IOException ex)
            {
                _logger.Log($"IO Exception copying: { sourcePath } to { destinationPath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log($"Unexpected FileOperation Exception copying { sourcePath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
        }

        /// <summary>
        /// Attempts to delete a flie
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>true if succeeds; false if not</returns>
        public bool DeleteFile(string filePath)
        {
            try
            {
                if (this.Exists(filePath))
                {
                    _fileSystem.File.Delete(filePath);
                    _logger.Log($"{ filePath } has been deleted");
                }
                else
                {
                    _logger.Log($"{ filePath } cannot be deleted as it does not exist");
                    return false;
                }
                
                return true;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log($"Path Too Long Exception deleting: { filePath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"Unauthorized Exception deleting: { filePath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (IOException ex)
            {
                _logger.Log($"IO Exception deleting: { filePath }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log($"Unexpected Exception deleting { filePath }: { ex.Message }");
                _logger.Log($"ERROR MESSAAGE: { ex.Message }");
                return false;
            }
        }

        /// <summary>
        /// Determines if a file exists...
        /// FileInfo creates a little more overhead, but now we have more information
        ///   as to whether or not a file exists 
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        /// <returns>true if file exists or logs reason and false if not</returns>
        public bool Exists(string pathToFile)
        {
            return _fileSystem.File.Exists(pathToFile);
        }                
    }
}
