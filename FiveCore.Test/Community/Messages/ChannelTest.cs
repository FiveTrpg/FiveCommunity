using Autofac;
using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers;
using FiveCore.Community.Utilities;
using NUnit.Framework;
using System;

namespace FiveCore.Test.Community.Messages
{
    public class ChannelTest
    {
        public IChannel Channel { get; set; }
        private IContainer Game { get; set; }
        public IPlayer Remilia { get; set; }
        public IChannelMessenger Messenger { get; set; }

        [SetUp]
        public void Setup()
        {
            Game = GameplayContainer.Game();

            Remilia = Game.Resolve<IPlayer>();
            Remilia.Identity = Guid.NewGuid().ToString();
            Remilia.Name = "Remilia";

            Messenger = Game.Resolve<IChannelMessenger>();
        }

        [Test]
        public void CreateChannel()
        {
            var result = Messenger.CreateChannel(Remilia, "", out var channel);

        }
    }
}
