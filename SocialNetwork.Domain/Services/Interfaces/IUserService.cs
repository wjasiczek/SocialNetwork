using SocialNetwork.Domain.Models;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Services
{
    public interface IUserService
    {
        Task<User> GetOrCreateUserAsync(string username);
        Task<User> GetUserAsync(string username);
    }
}