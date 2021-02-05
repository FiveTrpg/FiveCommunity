using System;

namespace FiveCore.Community.Gameplay.Messages.Abstraction.Channels
{
    public struct ChannelChat
    {
        public IChannelChatter From { get; set; }
        public IChannel To { get; set; }
        public IMessage Message { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }
}
