using SocialNetwork.Domain;
using SocialNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repositories
{
    public sealed class InMemoryUserRepository : IUserRepository
    {
        readonly ICollection<User> _usersStorage = new List<User>();

        public async Task<bool> ContainsAsync(Expression<Func<User, bool>> predicate) =>
            await Task.Run(() => _usersStorage.Where(predicate.Compile()).Any());

        public async Task CreateAsync(User user) =>
            await Task.Run(() => _usersStorage.Add(user));

        public async Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> predicate) =>
            await Task.Run(() => _usersStorage.Where(predicate.Compile()));
    }
}
