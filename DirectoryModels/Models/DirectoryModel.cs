using Models.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace Models.Models
{
    /// <summary>
    /// Model for a file system directory
    /// </summary>
    public class DirectoryModel : IDirectoryModel
    {
        public DirectoryInfoBase[] SubDirectories { get; set; }
        public List<DirectoryInfoBase> SubDirectoriesList { get; set; }
        public FileInfoBase[] Files { get; set; }
        public List<FileInfoBase> FilesList { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }        
        public string DirectoryPath { get; set; }
    }
}
