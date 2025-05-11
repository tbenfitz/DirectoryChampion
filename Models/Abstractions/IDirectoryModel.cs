using System;

namespace Models.Abstractions
{
    public interface IDirectoryModel
    {
        DateTime DateCreated { get; set; }
        string Path { get; set; }
    }
}