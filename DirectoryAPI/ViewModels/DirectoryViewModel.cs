using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryAPI.ViewModels
{
    public class DirectoryViewModel
    {
        public string DriveLetter { get; set; }
        public string DirectoryPath { get; set; }
        public object[] SubDirectories { get; set; }
        public DirectoryViewModel[] SubDirectoryModels { get; set; }
        public string[] Files { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public string SelectedDirectoryPath { get; set; }
        public string SelectedFilePath { get; set; }
        public string SearchTerms { get; set; }
        public object FileToUpload { get; set; }
    }
}
