using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using SocialNetwork.Domain.Services;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace SocialNetwork.Domain.Tests.Services
{
    [TestFixture]
    public class UserActionsServiceTest
    {
        UserActionsService _userActionsService;
        IUserService _userService;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _userService = Substitute.For<IUserService>();
            _userActionsService = new UserActionsService(_userService);
            _timeService = Substitute.For<ITimeService>();
        }

        public class PostMessageTest : UserActionsServiceTest
        {
            [TestCase("Bob", "Content")]
            public void ValidParameters_AddsNewMessageToUser(string username, string content)
            {
                var user = new User(username, _timeService);
                _userService
                    .GetOrCreateUserAsync(Arg.Any<string>())
                    .ReturnsForAnyArgs(user);

                _userActionsService.PostMessage(username, content).Wait();

                Assert.That(user.GetTimeline().Count == 1);
            }

            [TestCase("Bob", "")]
            [TestCase("Bob", " ")]
            [TestCase("Bob", null)]
            public void InvalidContent_ThrowsExceptionAndDoesNotAddNewMessage(string username, string content)
            {
                var user = new User(username, _timeService);
                _userService
                    .GetOrCreateUserAsync(Arg.Any<string>())
                    .ReturnsForAnyArgs(user);

                AsyncTestDelegate result = () => _userActionsService.PostMessage(username, content);

                Assert.That(user.GetTimeline().Count == 0);
                Assert.That(result,
                    Throws
                    .InstanceOf<MessageValidationException>()
                    .With
                    .Message
                    .EqualTo("One or more errors occurred. (Property Content cannot be null or whitespace.)"));
            }
        }

        public class ReadMessageTest : UserActionsServiceTest
        {
            [TestCase("Bob", "Content")]
            public void Username_MatchesExistingUser_ReturnsUsersTimeline(string username, string content)
            {
                var user = new User(username, _timeService);
                user.NewMessage(content);
                _userService
                    .GetUserAsync(Arg.Any<string>())
                    .ReturnsForAnyArgs(user);

                var result = _userActionsService.ReadMessages(username).Result;

                Assert.That(result.Count == 1);
            }

            [Test]
            public void Username_DoesNotMatchExistingUser_ReturnsEmptyList()
            {
                var result = _userActionsService.ReadMessages(Arg.Any<string>()).Result;

                Assert.That(result.Count == 0);
            }
        }

        public class FollowTest : UserActionsServiceTest
        {
            [TestCase("Subscriber", "Publisher")]
            public void ExistingUsers_SuccessfullyAddsPublisherToSubscriber(string subscriber, string publisher)
            {
                var user = new User(subscriber, _timeService);
                _userService
                    .GetOrCreateUserAsync(subscriber)
                    .Returns(user);
                _userService
                    .GetOrCreateUserAsync(publisher)
                    .Returns(new User(publisher, _timeService));

                _userActionsService.Follow(subscriber, publisher).Wait();

                Assert.That(user.GetPublishers().Count == 1);
            }
        }

        public class ReadWallTest : UserActionsServiceTest
        {
            [TestCase("Bob", "Content")]
            public void Username_MatchesExistingUser_ReturnsUsersWall(string username, string content)
            {
                var user = new User(username, _timeService);
                user.NewMessage(content);
                _userService
                    .GetUserAsync(Arg.Any<string>())
                    .ReturnsForAnyArgs(user);
                var publisher = new User("Publisher", _timeService);
                publisher.NewMessage(content);
                user.AddPublisher(publisher);

                var result = _userActionsService.ReadWall(username).Result;

                Assert.That(result.Count() == 2);
            }

            [Test]
            public void Username_DoesNotMatchExistingUser_ReturnsEmptyList()
            {
                var result = _userActionsService.ReadWall(Arg.Any<string>()).Result;

                Assert.That(result.Count() == 0);
            }
        }
    }
}
