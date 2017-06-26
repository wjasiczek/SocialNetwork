using SocialNetwork.Domain.Models;
using SocialNetwork.Formatting.Strategies;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;
using System;

namespace SocialNetwork.Tests.Formatting.Strategies
{
    [TestFixture]
    public class TimelineStrategyTest
    {
        TimelineStrategy _timelineStrategy;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = Substitute.For<ITimeService>();
            _timelineStrategy = new TimelineStrategy(_timeService);
        }

        public class FormatTest : TimelineStrategyTest
        {
            [Test]
            public void FormatsMessageCorrectly()
            {
                var timeNow = DateTime.UtcNow;
                _timeService.UtcNow().Returns(timeNow);
                var message = new Message("Content", timeNow.AddSeconds(-20),
                    new User("Bob", _timeService), _timeService);

                var result = _timelineStrategy.Format(message);

                Assert.That(result.Contains("Content (20 seconds ago)"));
            }
        }
    }
}
