using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Domain.Models
{
    public class User : ISelfValidating, IEquatable<User>
    {
        public string Username { get; }
        ICollection<Message> _timeline { get; } = new List<Message>();
        ISet<User> _publishers { get; } = new HashSet<User>();

        readonly ITimeService _timeService;

        internal User(string username, ITimeService timeService)
        {
            _timeService = timeService;
            Username = username;

            Validate();
        }

        internal IList<Message> GetTimeline() =>
            _timeline.ToList();

        internal IList<User> GetPublishers() =>
            _publishers.ToList();

        internal void AddPublisher(User publisher)
        {
            if (publisher != null && this != publisher)
                _publishers.Add(publisher);
            else
                throw new SubscriptionException($"Cannot subscribe user: {Username} to user: {publisher?.Username}.");
        }

        internal void NewMessage(string content) =>
            _timeline.Add(new Message(content, _timeService.UtcNow(), this, _timeService));

        internal IEnumerable<Message> GetAggregatedTimelines()
        {
            var wallUsers = GetPublishers();
            wallUsers.Add(this);

            return wallUsers.SelectMany(x => x.GetTimeline());
        }

        public virtual void Validate()
        {
            var exceptions = new List<Exception>();

            if (string.IsNullOrWhiteSpace(Username))
                exceptions.Add(new Exception($"Property {nameof(Username)} cannot be null or whitespace."));

            if (exceptions.Any())
                throw new UserValidationException(exceptions);
        }

        public bool Equals(User other) =>
            other != null &&
            Username == other.Username;

        public override bool Equals(object obj) =>
            Equals(obj as User);

        public override int GetHashCode() =>
            Username.GetHashCode();

        public static bool operator == (User user1, User user2)
        {
            if (ReferenceEquals(user1, null))
                return (ReferenceEquals(user2, null));

            return user1.Equals(user2);
        }

        public static bool operator != (User user1, User user2) =>
            !(user1 == user2);
    }
}
