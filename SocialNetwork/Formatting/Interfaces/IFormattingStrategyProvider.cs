using SocialNetwork.Formatting.Strategies;

namespace SocialNetwork.Formatting
{
    internal interface IFormattingStrategyProvider
    {
        IFormattingStrategy GetStrategy<T>() where T : BaseStrategy;
    }
}
