using Models.Abstractions;
using System;

namespace Models.Models
{
    public class DirectoryModel : IDirectoryModel
    {
        public DirectoryModel(ILogger logger)
        {
            _logger = logger;
        }

        ILogger _logger;

        public string Path { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
