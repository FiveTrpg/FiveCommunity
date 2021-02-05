using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers.Results;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Messages.Abstraction.Messengers
{
    public interface IChannelMessenger
    {
        public List<IChannel> Channels { get; set; }

        public ChannelCreateResult CreateChannel(IUnique creator, string password, out IChannel channel);
        public SendMessageResult SendMessage(ChannelChat chat);
    }
}
