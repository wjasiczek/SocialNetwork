using System;

namespace SocialNetwork.Core.EventArguments
{
    class FollowEventArgs : EventArgs
    {
        internal string SubscriberUsername { get; }
        internal string PublisherUsername { get; }

        internal FollowEventArgs(string subscriberUsername, string publisherUsername)
        {
            SubscriberUsername = subscriberUsername;
            PublisherUsername = publisherUsername;
        }
    }
}
