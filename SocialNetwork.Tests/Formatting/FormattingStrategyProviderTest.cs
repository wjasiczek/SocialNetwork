using SocialNetwork.Formatting;
using SocialNetwork.Formatting.Strategies;
using SocialNetwork.Infrastructure.Services;
using NSubstitute;
using NUnit.Framework;

namespace SocialNetwork.Tests.Formatting
{
    [TestFixture]
    public class FormattingStrategyProviderTest
    {
        FormattingStrategyProvider _formattingStrategyProvider;
        ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService = Substitute.For<ITimeService>();
            _formattingStrategyProvider = new FormattingStrategyProvider(_timeService);
        }

        public class GetStrategyTest : FormattingStrategyProviderTest
        {
            public void ReturnsCorrectStrategy()
            {
                var result = _formattingStrategyProvider.GetStrategy<TimelineStrategy>();

                Assert.That(result.GetType() == typeof(TimelineStrategy));
            }
        }
    }
}
