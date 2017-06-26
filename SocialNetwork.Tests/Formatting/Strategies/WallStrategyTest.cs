using SocialNetwork.Domain.Models;
using SocialNetwork.Formatting.Strategies;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;
using System;

namespace SocialNetwork.Tests.Formatting.Strategies
{
    [TestFixture]
    public class WallStrategyTest
    {
        WallStrategy _wallStrategy;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = Substitute.For<ITimeService>();
            _wallStrategy = new WallStrategy(_timeService);
        }

        public class FormatTest : WallStrategyTest
        {
            [Test]
            public void FormatsMessageCorrectly()
            {
                var timeNow = DateTime.UtcNow;
                _timeService.UtcNow().Returns(timeNow);
                var message = new Message("Content", timeNow.AddSeconds(-20), 
                    new User("Bob", _timeService), _timeService);

                var result = _wallStrategy.Format(message);

                Assert.That(result == "Bob - Content (20 seconds ago)");
            }
        }
    }
}
