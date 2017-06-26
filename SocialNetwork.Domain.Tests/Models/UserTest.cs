using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace SocialNetwork.Domain.Tests.Models
{
    [TestFixture]
    public class UserTest
    {
        User _user;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = Substitute.For<ITimeService>();
            _user = new User("Bob", _timeService);
        }

        public class NewMessageTest : UserTest
        {
            [Test]
            public void ValidContent_AddsNewMessageToTimeline()
            {
                _user.NewMessage("Content");

                Assert.That(_user.GetTimeline()[0].Content == "Content");
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void InvalidContent_ThrowsException_DoesNotAddMessageToTimeline(string content)
            {
                TestDelegate result = () => _user.NewMessage(content);

                Assert.That(_user.GetTimeline().Count == 0);
                Assert.That(result,
                    Throws
                    .InstanceOf<MessageValidationException>()
                    .With
                    .Message
                    .EqualTo("One or more errors occurred. (Property Content cannot be null or whitespace.)"));
            }
        }

        public class AddPublisher : UserTest
        {
            [Test]
            public void ValidUser_SuccessfullyAddsNewPublisher()
            {
                var user = new User("User1", _timeService);

                _user.AddPublisher(user);

                Assert.That(_user.GetPublishers()[0] == user);
            }

            [Test]
            public void UserNull_ThrowsException()
            {
                TestDelegate result = () => _user.AddPublisher(null);

                Assert.That(result,
                    Throws
                    .InstanceOf<SubscriptionException>()
                    .With
                    .Message
                    .EqualTo("Cannot subscribe user: Bob to user: ."));
            }

            [Test]
            public void TryingToSubscribeToSelf_ThrowsException()
            {
                TestDelegate result = () => _user.AddPublisher(_user);

                Assert.That(result,
                    Throws
                    .InstanceOf<SubscriptionException>()
                    .With
                    .Message
                    .EqualTo("Cannot subscribe user: Bob to user: Bob."));
            }
        }

        public class GetTimelineTest : UserTest
        {
            [Test]
            public void NoMessages_ReturnsEmptyList()
            {
                var result = _user.GetTimeline();

                Assert.That(result.Count == 0);
            }

            [Test]
            public void TwoMessagesAdded_ReturnsListWithTwoMessages()
            {
                _user.NewMessage("Content1");
                _user.NewMessage("Content2");

                var result = _user.GetTimeline();

                Assert.That(result[0].Content == "Content1");
                Assert.That(result[1].Content == "Content2");
            }
        }

        public class GetPublishersTest : UserTest
        {
            [Test]
            public void NoPublishers_ReturnsEmptyList()
            {
                var result = _user.GetPublishers();

                Assert.That(result.Count == 0);
            }

            [Test]
            public void TwoPublishersAdded_ReturnsListWithTwoPublishers()
            {
                _user.AddPublisher(new User("User1", _timeService));
                _user.AddPublisher(new User("User2", _timeService));

                var result = _user.GetPublishers();

                Assert.That(result[0].Username == "User1");
                Assert.That(result[1].Username == "User2");
            }
        }

        public class GetAggregatedTimelineTest : UserTest
        {
            [Test]
            public void UserSubscribed_ReturnsPersonalTimelineAndPublishersTimeline()
            {
                var user = new User("User1", _timeService);
                user.NewMessage("Content1");
                _user.NewMessage("Content2");
                _user.AddPublisher(user);

                var result = _user.GetAggregatedTimelines();

                Assert.That(result.Count() == 2);
                Assert.That(result.Any(x => x.Content == "Content1"));
                Assert.That(result.Any(x => x.Content == "Content2"));
            }

            [Test]
            public void UserNotSubscribed_ReturnsOnlyPersonalTimeline()
            {
                _user.NewMessage("Content1");

                var result = _user.GetAggregatedTimelines();

                Assert.That(result.ElementAt(0).Content == "Content1");
            }
        }

        public class ValidateTest : UserTest
        {
            [Test]
            public void UserValid_DoesNotThrowExceptions()
            {
                TestDelegate result = () => _user.Validate();

                Assert.DoesNotThrow(result);
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void UserInvalid_ThrowsException(string username)
            {
                TestDelegate result = () => { var user = new User(username, _timeService); };

                Assert.That(result,
                    Throws
                    .InstanceOf<UserValidationException>()
                    .With
                    .Message
                    .EqualTo("One or more errors occurred. (Property Username cannot be null or whitespace.)"));
            }
        }

        public class EqualsTest : UserTest
        {
            [Test]
            public void UsersWithSameUsername_ReturnsTrue()
            {
                var user = new User("Bob", _timeService);

                var result = _user.Equals(user);

                Assert.That(result);
            }

            [Test]
            public void UsersWithDifferentUsernames_ReturnsFalse()
            {
                var user = new User("NotBob", _timeService);

                var result = _user.Equals(user);

                Assert.That(!result);
            }

            [Test]
            public void UserNull_ReturnsFalse()
            {
                var result = _user.Equals(null);

                Assert.That(!result);
            }
        }

        public class EqualsOperatorTest : UserTest
        {
            [Test]
            public void UsersWithSameUsername_ReturnsTrue()
            {
                var user = new User("Bob", _timeService);

                var result = _user == user;

                Assert.That(result);
            }

            [Test]
            public void UsersWithDifferentUsernames_ReturnsFalse()
            {
                var user = new User("NotBob", _timeService);

                var result = _user == user;

                Assert.That(!result);
            }

            [Test]
            public void UserNull_ReturnsFalse()
            {
                var result = _user == null;

                Assert.That(!result);
            }
        }

        public class NotEqualOperatorTest : UserTest
        {
            [Test]
            public void UsersWithSameUsername_ReturnsFalse()
            {
                var user = new User("Bob", _timeService);

                var result = _user != user;

                Assert.That(!result);
            }

            [Test]
            public void UsersWithDifferentUsernames_ReturnsTrue()
            {
                var user = new User("NotBob", _timeService);

                var result = _user != user;

                Assert.That(result);
            }

            [Test]
            public void UserNull_ReturnsTrue()
            {
                var result = _user != null;

                Assert.That(result);
            }
        }
    }
}
