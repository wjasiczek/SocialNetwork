using SocialNetwork.Core;
using NUnit.Framework;

namespace SocialNetwork.Tests.Core
{
    [TestFixture]
    public class InputHandlerTest
    {
        InputHandler _inputHandler;

        [SetUp]
        public void SetUp()
        {
            _inputHandler = new InputHandler();
        }

        public class ProcessInput : InputHandlerTest
        {
            [Test]
            public void ContainsArrow_TriggersOnPostMessage()
            {
                var triggered = false;
                _inputHandler.OnPostMessageCommand += (sender, args) => triggered = true;

                _inputHandler.ProcessInput("Bob -> Hello World");

                Assert.That(triggered);
            }

            [Test]
            public void ContainsFollows_TriggersOnFollow()
            {
                var triggered = false;
                _inputHandler.OnFollowCommand += (sender, args) => triggered = true;

                _inputHandler.ProcessInput("Bob follows Alice");

                Assert.That(triggered);
            }

            [Test]
            public void ContainsWall_TriggersOnReadWall()
            {
                var triggered = false;
                _inputHandler.OnReadWallQuery += (sender, args) => triggered = true;

                _inputHandler.ProcessInput("Bob wall");

                Assert.That(triggered);
            }

            [Test]
            public void NoKeywords_TriggersOnReadTimeline()
            {
                var triggered = false;
                _inputHandler.OnReadTimelineQuery += (sender, args) => triggered = true;

                _inputHandler.ProcessInput("Bob");

                Assert.That(triggered);
            }
        }
    }
}
