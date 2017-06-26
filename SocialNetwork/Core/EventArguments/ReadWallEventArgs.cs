using System;

namespace SocialNetwork.Core.EventArguments
{
    class ReadWallEventArgs : EventArgs
    {
        internal string Username { get; }

        internal ReadWallEventArgs(string username) =>
            Username = username;
    }
}
