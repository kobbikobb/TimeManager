using System;

namespace TimeManager.Core
{
    public class DataIntegrityException : ApplicationException
    {
        public DataIntegrityException(string message) :  base(message)
        {
            
        }
    }
}
