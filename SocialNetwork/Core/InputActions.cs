using SocialNetwork.Core.EventArguments;
using SocialNetwork.Domain.Services;
using SocialNetwork.Formatting;
using SocialNetwork.Formatting.Strategies;
using System;
using System.Linq;

namespace SocialNetwork.Core
{
    static class InputActions
    {
        internal static EventHandler<PostMessageEventArgs> PostMessage(IUserActionsService userActionsService) =>
            (sender, arguments) => userActionsService.PostMessage(arguments.Username, arguments.Content);

        internal static EventHandler<FollowEventArgs> Follow(IUserActionsService userActionsService) =>
            (sender, arguments) => userActionsService.Follow(arguments.SubscriberUsername, arguments.PublisherUsername);

        internal static EventHandler<ReadTimelineEventArgs> ReadTimeline(IUserActionsService userActionsService, IFormattingStrategyProvider formattingStrategyProvider) =>
            async (sender, arguments) =>
            {
                var messages = (await userActionsService
                    .ReadMessages(arguments.Username)
                    .ConfigureAwait(false))?
                    .OrderByDescending(x => x.CreateDateUTC);

                foreach (var message in messages)
                    Console.WriteLine(formattingStrategyProvider.GetStrategy<TimelineStrategy>().Format(message));
            };

        internal static EventHandler<ReadWallEventArgs> ReadWall(IUserActionsService userActionsService, IFormattingStrategyProvider formattingStrategyProvider) =>
            async (sender, arguments) =>
            {
                var messages = (await userActionsService
                    .ReadWall(arguments.Username)
                    .ConfigureAwait(false))?
                    .OrderByDescending(x => x.CreateDateUTC);

                foreach (var message in messages)
                    Console.WriteLine(formattingStrategyProvider.GetStrategy<TimelineStrategy>().Format(message));
            };
    }
}
