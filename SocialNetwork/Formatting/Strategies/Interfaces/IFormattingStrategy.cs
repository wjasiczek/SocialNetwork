using SocialNetwork.Domain.Models;

namespace SocialNetwork.Formatting.Strategies
{
    internal interface IFormattingStrategy
    {
        string Format(Message message);
    }
}
