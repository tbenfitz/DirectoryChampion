using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectoryOperations.Utilities
{
    internal static class FileSystemUtilities
    {
        internal static string GetPathToFilesDirectory(string pathTofile)
        {
            return Path.GetDirectoryName(pathTofile);
        }
    }
}
