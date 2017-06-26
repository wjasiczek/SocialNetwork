using SocialNetwork.Core;
using SocialNetwork.Domain.Services;
using SocialNetwork.Formatting;
using System;

namespace SocialNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            DiContainer.Initialise();

            var inputHandler = SetUpInputHandler();

            while (true)
            {
                var input = Console.ReadLine();
                inputHandler.ProcessInput(input);
            }
        }

        static InputHandler SetUpInputHandler()
        {
            var userActionsService = DiContainer.Resolve<IUserActionsService>();
            var formattingStrategyProvider = DiContainer.Resolve<IFormattingStrategyProvider>();

            var inputHandler = new InputHandler();

            inputHandler.OnPostMessageCommand += InputActions.PostMessage(userActionsService);
            inputHandler.OnFollowCommand += InputActions.Follow(userActionsService);
            inputHandler.OnReadTimelineQuery += InputActions.ReadTimeline(userActionsService, formattingStrategyProvider);
            inputHandler.OnReadWallQuery += InputActions.ReadWall(userActionsService, formattingStrategyProvider);

            return inputHandler;
        }
    }
}