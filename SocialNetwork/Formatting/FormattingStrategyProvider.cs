using SocialNetwork.Formatting.Strategies;
using SocialNetwork.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Formatting
{
    sealed class FormattingStrategyProvider : IFormattingStrategyProvider
    {
        public FormattingStrategyProvider(ITimeService timeService)
        {
            RegisteredStrategies = new Dictionary<Type, IFormattingStrategy>
            {
                { typeof(TimelineStrategy), new TimelineStrategy(timeService) },
                { typeof(WallStrategy), new WallStrategy(timeService) }
            };
        }

        public IFormattingStrategy GetStrategy<T>() where T : BaseStrategy =>
            RegisteredStrategies[typeof(T)];

        private Dictionary<Type, IFormattingStrategy> RegisteredStrategies; 
    }
}
