using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace Models.Abstractions
{
    /// <summary>
    /// Contract for directory model
    /// </summary>
    public interface IDirectoryModel
    {
        DirectoryInfoBase[] SubDirectories { get; set; }
        List<DirectoryInfoBase> SubDirectoriesList { get; set; }
        FileInfoBase[] Files { get; set; }
        List<FileInfoBase> FilesList { get; set; }
        DateTime DateCreated { get; set; }
        DateTime LastModified { get; set; }
        string DirectoryPath { get; set; }
    }
}