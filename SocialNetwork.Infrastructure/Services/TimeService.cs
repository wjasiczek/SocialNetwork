using System;

namespace SocialNetwork.Infrastructure.Services
{
    public class TimeService : ITimeService
    {
        public DateTime UtcNow() =>
            DateTime.UtcNow;
    }
}
