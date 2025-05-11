using DirectoryOperations.Utilities;
using DirectoryOperations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Models.Abstractions;
using Models.Models;
using System.IO.Abstractions;

namespace DirectoryOperations.Classes
{
    /// <summary>
    /// Factory for creating DirectoryOperations classes
    /// </summary>
    public static class Factory
    {
        public static IDirectoryList CreateDirectoryList()
        {
            return new DirectoryList(Factory.CreateFileSystem(),
                                     Factory.CreateDirectoryModel(),
                                     Factory.CreateLogger(), 
                                     Factory.CreateExceptionHandler());
        }
        
        public static IDirectoryOperations CreateDirectoryOperations()
        {
            return new DirectoryOperations(Factory.CreateFileSystem(),
                                           Factory.CreateLogger(),
                                           Factory.CreateExceptionHandler());
        }

        public static IFileOperations CreateFileOperations()
        {
            return new FileOperations(Factory.CreateFileSystem(),
                                      Factory.CreateLogger(), 
                                      Factory.CreateExceptionHandler());
        }

        public static IDirectoryModel CreateDirectoryModel()
        {
            return new DirectoryModel();
        }

        public static IFileSystem CreateFileSystem()
        {
            return new FileSystem();
        }

        public static IException CreateExceptionHandler()
        {
            return new ExceptionHandler(Factory.CreateLogger());
        }

        public static ILogger CreateLogger()
        {
            return new Utilities.Logger();
        }       
    }
}
