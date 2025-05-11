using Models.Abstractions;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Classes
{
    /// <summary>
    /// Factory class for Models
    /// </summary>
    public static class Factory
    {
        public static DirectoryModel CreateDirectoryModel()
        {
            return new DirectoryModel();
        }                
    }
}
