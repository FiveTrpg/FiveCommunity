using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;

namespace FiveCore.Community.Gameplay.Messages
{
    public interface IChannelChatterFactory
    {
        public IChannelChatter CreateChatterFromUnique(IChannel channel, IUnique unique);
    }
}
