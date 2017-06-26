using SocialNetwork.Core.EventArguments;
using System;
using System.Linq;

namespace SocialNetwork.Core
{
    internal sealed class InputHandler
    {
        internal event EventHandler<PostMessageEventArgs> OnPostMessageCommand;
        internal event EventHandler<FollowEventArgs> OnFollowCommand;
        internal event EventHandler<ReadTimelineEventArgs> OnReadTimelineQuery;
        internal event EventHandler<ReadWallEventArgs> OnReadWallQuery;

        private void PostNewMessage(string username, string content) =>
            OnPostMessageCommand?.Invoke(this, new PostMessageEventArgs(username, content));

        private void Follow(string subscriberUsername, string publisherUsername) =>
            OnFollowCommand?.Invoke(this, new FollowEventArgs(subscriberUsername, publisherUsername));

        private void ReadTimeline(string username) =>
            OnReadTimelineQuery?.Invoke(this, new ReadTimelineEventArgs(username));

        private void ReadWall(string username) =>
            OnReadWallQuery?.Invoke(this, new ReadWallEventArgs(username));

        internal void ProcessInput(string input)
        {
            var formattedInput = input.Split(' ');

            if (input.Contains("->"))
                PostNewMessage(formattedInput[0], string.Join(" ", formattedInput.Skip(2)));
            else if (input.Contains("follows"))
                Follow(formattedInput[0], formattedInput[2]);
            else if (input.Contains("wall"))
                ReadWall(formattedInput[0]);
            else
                ReadTimeline(formattedInput[0]);
        }
    }
}
