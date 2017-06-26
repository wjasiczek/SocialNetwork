using System;

namespace SocialNetwork.Core.EventArguments
{
    class ReadTimelineEventArgs : EventArgs
    {
        internal string Username { get; }

        internal ReadTimelineEventArgs(string username) =>
            Username = username;
    }
}
