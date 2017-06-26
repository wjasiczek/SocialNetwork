using SocialNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetwork.Domain
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> predicate);
        Task<bool> ContainsAsync(Expression<Func<User, bool>> predicate);
    }
}
