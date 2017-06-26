using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Services
{
    public sealed class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly ITimeService _timeService;

        public UserService(
            IUserRepository userRepository, 
            ITimeService timeService)
        {
            _userRepository = userRepository;
            _timeService = timeService;
        }

        public async Task<User> GetOrCreateUserAsync(string username)
        {
            var isExistingUser = await _userRepository
                .ContainsAsync(x => x.Username == username)
                .ConfigureAwait(false);

            return isExistingUser ? 
                await GetUserAsync(username).ConfigureAwait(false) : 
                await CreateUserAsync(username).ConfigureAwait(false);
        }

        public async Task<User> GetUserAsync(string username) =>
            (await _userRepository.GetAsync(x => x.Username == username)
                .ConfigureAwait(false))
                .FirstOrDefault();

        private async Task<User> CreateUserAsync(string username)
        {
            var user = new User(username, _timeService);
            await _userRepository
                .CreateAsync(user)
                .ConfigureAwait(false);

            return user;
        }
    }
}
