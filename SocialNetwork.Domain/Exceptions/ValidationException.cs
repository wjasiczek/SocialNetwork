using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Exceptions
{
    public class ValidationException : AggregateException
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(IEnumerable<Exception> innerExceptions) : base(innerExceptions)
        {
        }

        public ValidationException(params Exception[] innerExceptions) : base(innerExceptions)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValidationException(string message, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions)
        {
        }

        public ValidationException(string message, params Exception[] innerExceptions) : base(message, innerExceptions)
        {
        }
    }
}
