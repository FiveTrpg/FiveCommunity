using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels.Results;
using System;

namespace FiveCore.Community.Gameplay.Messages.Abstraction.Channels
{
    public interface IChannelChatter
    {
        public IChannel Channel { get; set; }
        public IUnique Unique { get; set; }
        public bool Muted { get; set; }

        public event Action<ChannelChat> OnReceive;
        public ChatSendResult SendMessage(IMessage message);
    }
}
