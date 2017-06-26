using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;
using System;

namespace SocialNetwork.Domain.Tests.Models
{
    [TestFixture]
    public class MessageTest
    {
        ITimeService _timeService;

        [SetUp]
        public void SetUp() =>
            _timeService = Substitute.For<ITimeService>();

        public class ValidateTest : MessageTest
        {
            [Test]
            public void MessageValid_DoesNotThrowException()
            {
                TestDelegate result = () => { var message = new Message("Content", _timeService.UtcNow(),
                    new User("Bob", _timeService), _timeService); };

                Assert.DoesNotThrow(result);
            }

            [Test]
            public void MessageInvalid_ThrowsException()
            {
                TestDelegate result = () => { var message = new Message("", _timeService.UtcNow().AddDays(1), null, _timeService); };

                Assert.That(result,
                    Throws
                    .InstanceOf<MessageValidationException>()
                    .With
                    .Message
                    .EqualTo("One or more errors occurred. (Property User cannot be null.) (Property Content cannot be null or whitespace.) (Property CreateDateUTC cannot be in the future.)"));
            }
        }
    }
}
