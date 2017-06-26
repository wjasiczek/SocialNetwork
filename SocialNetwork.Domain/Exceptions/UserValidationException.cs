using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserValidationException : ValidationException
    {
        public UserValidationException()
        {
        }

        public UserValidationException(string message) : base(message)
        {
        }

        public UserValidationException(IEnumerable<Exception> innerExceptions) : base(innerExceptions)
        {
        }

        public UserValidationException(params Exception[] innerExceptions) : base(innerExceptions)
        {
        }

        public UserValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserValidationException(string message, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions)
        {
        }

        public UserValidationException(string message, params Exception[] innerExceptions) : base(message, innerExceptions)
        {
        }
    }
}
