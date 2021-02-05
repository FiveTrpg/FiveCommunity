using Autofac;
using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Characters.Npcs;
using FiveCore.Community.Gameplay.Messages;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers;
using FiveCore.Community.Gameplay.Parties;
using FiveCore.Community.Gameplay.Parties.Abstraction;

namespace FiveCore.Community.Utilities
{
    public static class GameplayContainer
    {
        public static IContainer Game()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Lobby>().As<ILobby>().SingleInstance();
            builder.RegisterType<Core>().As<ICore>().SingleInstance();
            builder.RegisterType<PartyFactory>().As<IPartyFactory>().SingleInstance();
            builder.RegisterType<Player>().As<IPlayer>().InstancePerDependency();
            builder.RegisterType<Party>().As<IParty>().InstancePerDependency();
            builder.RegisterType<ChatterFactory>().As<IChannelChatterFactory>().SingleInstance();
            builder.RegisterType<Chatter>().As<IChannelChatter>().InstancePerDependency();
            builder.RegisterType<Channel>().As<IChannel>().InstancePerDependency();
            builder.RegisterType<Messenger>().As<IChannelMessenger>().SingleInstance();
            return builder.Build();
        }
    }
}
