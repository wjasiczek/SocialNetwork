using SocialNetwork.Data.Repositories;
using SocialNetwork.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;

namespace SocialNetwork.Data.Test.Repositories
{
    [TestFixture]
    public class InMemoryUserRepositoryTest
    {
        InMemoryUserRepository _repository;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _repository = new InMemoryUserRepository();
            _timeService = Substitute.For<ITimeService>();
        }

        public class CreateAsyncTest : InMemoryUserRepositoryTest
        {
            [Test]
            public void ValidUser_SuccessfullyAddsUser()
            {
                _repository.CreateAsync(new User("Bob", _timeService)).Wait();
                var user = _repository.GetAsync(x => x.Username == "Bob").Result.FirstOrDefault();

                Assert.That(user != null);
                Assert.That(user.Username == "Bob");
            }
        }

        public class ContainsAsyncTest : InMemoryUserRepositoryTest
        {
            [Test]
            public void UserExists_ReturnsTrue()
            {
                _repository.CreateAsync(new User("Bob", _timeService)).Wait();

                var result = _repository.ContainsAsync(x => x.Username == "Bob").Result;

                Assert.That(result);
            }

            [Test]
            public void UserDoesNotExist_ReturnsFalse()
            {
                var result = _repository.ContainsAsync(x => x.Username == "Bob").Result;

                Assert.That(!result);
            }
        }

        public class GetAsyncTest : InMemoryUserRepositoryTest
        {
            [Test]
            public void UserExists_ReturnsUser()
            {
                _repository.CreateAsync(new User("Bob", _timeService)).Wait();

                var result = _repository.GetAsync(x => x.Username == "Bob").Result.FirstOrDefault();

                Assert.That(result != null);
                Assert.That(result.Username == "Bob");
            }

            [Test]
            public void UsersExist_ReturnsUsers()
            {
                _repository.CreateAsync(new User("Bob1", _timeService)).Wait();
                _repository.CreateAsync(new User("Bob2", _timeService)).Wait();

                var result = _repository.GetAsync(x => x.Username.StartsWith("Bob")).Result;

                Assert.That(result.Count() == 2);
                Assert.That(result.ElementAt(0).Username == "Bob1");
                Assert.That(result.ElementAt(1).Username == "Bob2");
            }

            [Test]
            public void UserDoesNotExist_ReturnsEmptyCollection()
            {
                var result = _repository.GetAsync(x => x.Username == "Bob").Result;

                Assert.That(result.Count() == 0);
            }
        }
    }
}
