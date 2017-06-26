using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Exceptions
{
    public class MessageValidationException : ValidationException
    {
        public MessageValidationException()
        {
        }

        public MessageValidationException(string message) : base(message)
        {
        }

        public MessageValidationException(IEnumerable<Exception> innerExceptions) : base(innerExceptions)
        {
        }

        public MessageValidationException(params Exception[] innerExceptions) : base(innerExceptions)
        {
        }

        public MessageValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MessageValidationException(string message, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions)
        {
        }

        public MessageValidationException(string message, params Exception[] innerExceptions) : base(message, innerExceptions)
        {
        }
    }
}
