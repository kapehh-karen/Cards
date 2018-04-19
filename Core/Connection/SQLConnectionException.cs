using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Connection
{
    public class SQLConnectionException : Exception
    {
        public SQLConnectionException(string message) : base(message)
        {
        }
    }
}
