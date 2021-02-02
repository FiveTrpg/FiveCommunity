using FiveCore.Community.Gameplay.Characters;
using System;

namespace FiveCore.Community.Gameplay.Messages.Channels
{
    public interface IChannelChatter
    {
        public IChannel Channel { get; set; }
        public IUnique Unique { get; set; }
        public bool Muted { get; set; }

        public event Action<ChannelChat> OnReceive;
        public void SendMessage(IMessage message);
    }
}
