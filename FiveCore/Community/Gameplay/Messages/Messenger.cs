using Autofac;
using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels;
using FiveCore.Community.Gameplay.Messages.Abstraction.Channels.Results;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers.Results;
using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Messages
{
    public class Messenger : IChannelMessenger
    {
        public List<IChannel> Channels { get; set; }

        private ILifetimeScope Scope { get; set; }

        public Messenger(ILifetimeScope scope)
        {
            Scope = scope;
        }

        public ChannelCreateResult CreateChannel(IUnique creator, string password, out IChannel channel)
        {
            var emptyChannel = Scope.Resolve<IChannel>();
            emptyChannel.Password = password;
            if (emptyChannel.Join(creator, password, out var chatter) != ChannelJoinResult.Success)
            {
                channel = null;
                return ChannelCreateResult.CreatorJoinFail;
            }

            emptyChannel.Owner = chatter;
            channel = emptyChannel;
            Channels.Add(emptyChannel);
            return ChannelCreateResult.Success;
        }

        public SendMessageResult SendMessage(ChannelChat chat)
        {
            throw new NotImplementedException();
        }
    }
}
