using System;

namespace WebApiProblem.Tests.Json
{
    internal class DynamicJsonObjectReadException : Exception
    {
        public DynamicJsonObjectReadException()
        {
        }

        public DynamicJsonObjectReadException(string message) : base(message)
        {
        }

        public DynamicJsonObjectReadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}