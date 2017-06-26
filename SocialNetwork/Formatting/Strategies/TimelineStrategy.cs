using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Services;

namespace SocialNetwork.Formatting.Strategies
{
    sealed class TimelineStrategy : BaseStrategy, IFormattingStrategy
    {
        internal TimelineStrategy(ITimeService timeService) : base(timeService) { }

        public string Format(Message message) =>
            $"{message.Content} ({GetTimeSince(message.CreateDateUTC)})";
    }
}
