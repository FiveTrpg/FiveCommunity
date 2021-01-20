using Autofac;
using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Characters.Npcs;
using FiveCore.Community.Gameplay.Parties;

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
            return builder.Build();
        }
    }
}
