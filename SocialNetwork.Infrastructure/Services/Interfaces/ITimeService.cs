using System;

namespace SocialNetwork.Infrastructure.Services
{
    public interface ITimeService
    {
        DateTime UtcNow();
    }
}
