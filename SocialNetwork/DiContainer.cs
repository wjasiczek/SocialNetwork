using SocialNetwork.Data.Repositories;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Services;
using SocialNetwork.Formatting;
using SocialNetwork.Infrastructure.Services;
using SimpleInjector;

namespace SocialNetwork
{
    static class DiContainer
    {
        private static Container _container;

        internal static Container Instance => _container ?? (_container = new Container());

        internal static void Initialise()
        {
            Instance.Register<ITimeService, TimeService>(Lifestyle.Singleton);
            Instance.Register<IUserRepository, InMemoryUserRepository>(Lifestyle.Singleton);
            Instance.Register<IUserActionsService, UserActionsService>(Lifestyle.Singleton);
            Instance.Register<IUserService, UserService>(Lifestyle.Singleton);
            Instance.Register<IFormattingStrategyProvider, FormattingStrategyProvider>(Lifestyle.Singleton);

            Instance.Verify();
        }

        internal static T Resolve<T>() where T : class
        {
            return Instance.GetInstance<T>();
        }
    }
}
