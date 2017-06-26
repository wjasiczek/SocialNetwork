using System;

namespace SocialNetwork.Core.EventArguments
{
    class PostMessageEventArgs : EventArgs
    {
        internal string Username { get; }
        internal string Content { get; }

        internal PostMessageEventArgs(string username, string content)
        {
            Username = username;
            Content = content;
        }
    }
}
