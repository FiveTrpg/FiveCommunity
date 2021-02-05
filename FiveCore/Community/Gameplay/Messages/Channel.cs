using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Messages
{
    public class Channel : IChannel
    {
        public Dictionary<string, IChannelChatter> Chatters { get; set; }
        public string Name { get; set; }
        public IChannelChatter Owner { get; set; }
        public string Password { get; set; }
        IChannelChatterFactory IChannel.ChatterFactory { get; set; }
        IChannelMessenger IChannel.Messenger { get; set; }
    }
}
