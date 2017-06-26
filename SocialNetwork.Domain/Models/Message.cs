using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Domain.Models
{
    public class Message : ISelfValidating
    {
        public string Content { get; }
        public DateTime CreateDateUTC { get; }
        public User User { get; }

        ITimeService _timeService;

        internal Message(
            string content, 
            DateTime createDateUTC,
            User user,
            ITimeService timeService)
        {
            _timeService = timeService;
            Content = content;
            CreateDateUTC = createDateUTC;
            User = user;

            Validate();
        }

        public virtual void Validate()
        {
            var exceptions = new List<Exception>();

            if (User == null)
                exceptions.Add(new Exception($"Property {nameof(User)} cannot be null."));
            if (string.IsNullOrWhiteSpace(Content))
                exceptions.Add(new Exception($"Property {nameof(Content)} cannot be null or whitespace."));
            if (CreateDateUTC > _timeService.UtcNow())
                exceptions.Add(new Exception($"Property {nameof(CreateDateUTC)} cannot be in the future."));

            if (exceptions.Any())
                throw new MessageValidationException(exceptions);
        }
    }
}
