using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Services;

namespace SocialNetwork.Formatting.Strategies
{
    sealed class WallStrategy : BaseStrategy, IFormattingStrategy
    {
        internal WallStrategy(ITimeService timeService) : base(timeService) { }

        public string Format(Message message) =>
            $"{message.User.Username} - {message.Content} ({GetTimeSince(message.CreateDateUTC)})";
    }
}
