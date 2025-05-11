using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryOperations.Abstractions
{
    public interface IException
    {
        Object Exception { get; }
        void SetException(object ex);
        void HandleException(string message);
        bool HandleBoolException(string message);
    }
}
