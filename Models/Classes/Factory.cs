using Model.Utilities;
using Models.Abstractions;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryOperations.Classes
{
    public static class Factory
    {
        public static DirectoryModel CreateDirectoryModel()
        {
            return new DirectoryModel(Factory.CreateLogger());
        }        

        public static ILogger CreateLogger()
        {
            return new Logger();
        }
    }
}
