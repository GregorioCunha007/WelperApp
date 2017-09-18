using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTryout.Model.Exceptions
{
    public class GpsDisabledException : Exception
    {
        public GpsDisabledException()
        {
        }

        public GpsDisabledException(string message) : base(message)
        {
        }

        public GpsDisabledException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class GeneralNetworkException : Exception
    {
        public GeneralNetworkException()
        {
        }

        public GeneralNetworkException(string message) : base(message)
        {
        }

        public GeneralNetworkException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class PasswordMismatchException : Exception
    {
        public PasswordMismatchException()
        {
        }

        public PasswordMismatchException(string message) : base(message)
        {
        }

        public PasswordMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
