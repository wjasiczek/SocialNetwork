using System;

namespace SocialNetwork.Domain.Exceptions
{
    public class SubscriptionException : InvalidOperationException
    {
        public SubscriptionException()
        {
        }

        public SubscriptionException(string message) : base(message)
        {
        }

        public SubscriptionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
