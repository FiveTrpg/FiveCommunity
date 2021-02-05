using Autofac;
using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;

namespace FiveCore.Community.Gameplay.Messages
{
    public class ChatterFactory : IChannelChatterFactory
    {
        public ILifetimeScope Scope { get; set; }
        public ChatterFactory(ILifetimeScope scope)
        {
            this.Scope = scope;
        }
        public IChannelChatter CreateChatterFromUnique(IChannel channel, IUnique unique)
        {
            var emptyChatter = Scope.Resolve<IChannelChatter>();
            emptyChatter.Unique = unique;
            emptyChatter.Channel = channel;
            return emptyChatter;
        }
    }
}
