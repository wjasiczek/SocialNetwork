using SocialNetwork.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Services
{
    public interface IUserActionsService
    {
        Task Follow(string subscriberUsername, string targetUsername);
        Task PostMessage(string username, string content);
        Task<ICollection<Message>> ReadMessages(string username);
        Task<IEnumerable<Message>> ReadWall(string username);
    }
}