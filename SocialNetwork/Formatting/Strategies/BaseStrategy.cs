using SocialNetwork.Infrastructure.Services;
using System;

namespace SocialNetwork.Formatting.Strategies
{
    internal abstract class BaseStrategy
    {
        readonly ITimeService _timeService;

        internal BaseStrategy(ITimeService timeService) =>
            _timeService = timeService;

        protected virtual string GetTimeSince(DateTime dateTimeUTC)
        {
            var timeDifference = _timeService.UtcNow() - dateTimeUTC;

            if (timeDifference < TimeSpan.FromMinutes(1))
                return timeDifference.Seconds == 1 ? "one second ago" : timeDifference.Seconds + " seconds ago";
            else if (timeDifference < TimeSpan.FromHours(1))
                return timeDifference.Minutes == 1 ? "one minute ago" : timeDifference.Minutes + " minutes ago";
            else if (timeDifference < TimeSpan.FromDays(1))
                return timeDifference.Hours == 1 ? "one hour ago" : timeDifference.Hours + " hours ago";
            else
                return timeDifference.Days == 1 ? "one day ago" : timeDifference.Days + " days ago";
        }
    }
}
