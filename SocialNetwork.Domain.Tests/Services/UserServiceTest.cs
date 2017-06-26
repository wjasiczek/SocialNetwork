using SocialNetwork.Domain.Models;
using SocialNetwork.Domain.Services;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace SocialNetwork.Domain.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        UserService _userService;
        IUserRepository _userRepository;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _timeService = Substitute.For<ITimeService>();
            _userService = new UserService(_userRepository, _timeService);
        }

        public class GetUserAsync : UserServiceTest
        {
            [TestCase("Bob")]
            public void Username_MatchesExistingUser_ReturnsUser(string username)
            {
                _userRepository
                    .GetAsync(Arg.Any<Expression<Func<User, bool>>>())
                    .ReturnsForAnyArgs(new[] { new User(username, _timeService) });

                var result = _userService.GetUserAsync(username).Result;

                _userRepository.ReceivedWithAnyArgs().GetAsync(Arg.Any<Expression<Func<User, bool>>>());
                Assert.That(result.Username == username);
            }

            [Test]
            public void Username_DoesNotMatchExistingUser_ReturnsNull()
            {
                var result = _userService.GetUserAsync(Arg.Any<string>()).Result;

                Assert.That(result == null);
            }
        }

        public class GetOrCreateUserAsync : UserServiceTest
        {
            [TestCase("Bob")]
            public void Username_MatchesExistingUser_ReturnsUser(string username)
            {
                _userRepository
                    .ContainsAsync(Arg.Any<Expression<Func<User, bool>>>())
                    .ReturnsForAnyArgs(true);
                _userRepository
                    .GetAsync(Arg.Any<Expression<Func<User, bool>>>())
                    .ReturnsForAnyArgs(new[] { new User(username, _timeService) });

                var result = _userService.GetOrCreateUserAsync(username).Result;

                Assert.That(result != null);
            }

            [TestCase("Bob")]
            public void Username_DoesNotMatchExistingUser_ReturnsUser(string username)
            {
                _userRepository
                    .ContainsAsync(Arg.Any<Expression<Func<User, bool>>>())
                    .ReturnsForAnyArgs(false);

                var result = _userService.GetOrCreateUserAsync(username).Result;

                Assert.That(result != null);
            }
        }
    }
}
