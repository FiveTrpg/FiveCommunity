using FiveCore.Community.Gameplay.Characters;

namespace FiveCore.Community.Gameplay.Messages.Channels
{
    public interface IChannelChatterFactory
    {
        public IChannelChatter CreateChatterFromUnique(IUnique unique);
    }
}
