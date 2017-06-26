using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Services
{
    public sealed class UserActionsService : IUserActionsService
    {
        readonly IUserService _userService;

        public UserActionsService(IUserService userService) =>
            _userService = userService;

        public async Task PostMessage(string username, string content)
        {
            var user = await _userService
                .GetOrCreateUserAsync(username)
                .ConfigureAwait(false);

            user.NewMessage(content);
        }

        public async Task<ICollection<Message>> ReadMessages(string username)
        {
            var user = await _userService
                .GetUserAsync(username)
                .ConfigureAwait(false);

            return user == null ? 
                new List<Message>() : 
                user.GetTimeline();
        }

        public async Task Follow(string subscriberUsername, string publisherUsername)
        {
            var subscriber = await _userService
                .GetOrCreateUserAsync(subscriberUsername)
                .ConfigureAwait(false);
            var publisher = await _userService
                .GetOrCreateUserAsync(publisherUsername)
                .ConfigureAwait(false);

            subscriber?.AddPublisher(publisher);
        }

        public async Task<IEnumerable<Message>> ReadWall(string username)
        {
            var user = await _userService
                .GetUserAsync(username)
                .ConfigureAwait(false);

            return user == null ? 
                new List<Message>() : 
                user.GetAggregatedTimelines();
        }
    }
}
