using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels.Results;
using System;

namespace FiveCore.Community.Gameplay.Messages
{
    public class Chatter : IChannelChatter
    {
        public IUnique Unique { get; set; }
        public bool Muted { get; set; }
        public IChannel Channel { get; set; }

        public event Action<ChannelChat> OnReceive;

        public ChatSendResult SendMessage(IMessage message)
        {
            return Channel.SendMessage(this, message);
        }
    }
}
