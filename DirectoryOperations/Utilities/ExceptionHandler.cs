using DirectoryOperations.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DirectoryOperations.Utilities
{
    public class ExceptionHandler : IException
    { 
        public ExceptionHandler(ILogger logger)
        {
            _logger = logger;
            stackTrace = new StackTrace();
        }

        ILogger _logger;
        object _exception;
        StackTrace stackTrace;

        public Object Exception { get; private set; }

        public void SetException(object ex)
        {
            _exception = ex;
        }

        public void HandleException(string message)
        {
            this.LogException(message);
        }

        public bool HandleBoolException(string message)
        {
            this.LogException(message);            

            return false;
        }

        private void LogException(string message)
        {
            _logger.Log(message);
        }
    }
}
