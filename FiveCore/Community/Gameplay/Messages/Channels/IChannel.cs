using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Channels.Results;
using FiveCore.Community.Gameplay.Messages.Messengers;
using FiveCore.Community.Gameplay.Messages.Messengers.Results;
using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Messages.Channels
{
    public interface IChannel
    {
        protected IChannelChatterFactory ChatterFactory { get; set; }
        protected IChannelMessenger Messenger { get; set; }
        public Dictionary<string, IChannelChatter> Chatters { get; set; }

        public string Name { get; set; }
        public IChannelChatter Owner { get; set; }
        public string Password { get; set; }

        public ChannelJoinResult Join(IUnique unique, string password, out IChannelChatter chatter)
        {
            chatter = null;
            if (Chatters.ContainsKey(unique.Identity)) return ChannelJoinResult.AlreadyInChannel;
            if (password != Password) return ChannelJoinResult.IncorrectPassword;

            chatter = ChatterFactory.CreateChatterFromUnique(unique);
            Chatters.Add(unique.Identity, chatter);
            return ChannelJoinResult.Success;
        }
        public ChatterMuteResult Mute(IChannelChatter @operator, IChannelChatter aim)
        {
            if (@operator != Owner) return ChatterMuteResult.NotOwner;
            if (aim.Muted) return ChatterMuteResult.AlreadyMute;

            aim.Muted = true;
            return ChatterMuteResult.Success;
        }

        public ChatterMuteResult Unmute(IChannelChatter @operator, IChannelChatter aim)
        {
            if (@operator != Owner) return ChatterMuteResult.NotOwner;
            if (!aim.Muted) return ChatterMuteResult.NotMute;

            aim.Muted = false;
            return ChatterMuteResult.Success;
        }

        public ChannelLeaveResult Leave(IChannelChatter chatter) => Leave(chatter.Unique);
        public ChannelLeaveResult Leave(IUnique unique)
        {
            if (!Chatters.ContainsKey(unique.Identity)) return ChannelLeaveResult.NotInChannel;

            Chatters.Remove(unique.Identity);
            return ChannelLeaveResult.Success;
        }

        public ChatSendResult SendMessage(IChannelChatter chatter, IMessage message)
        {
            if (chatter.Muted) return ChatSendResult.Muted;
            if (!Chatters.ContainsKey(chatter.Unique.Identity)) return ChatSendResult.NotInChannel;

            var result = Messenger.SendMessage(new ChannelChat()
            {
                To = this,
                From = chatter,
                Message = message,
                SentAt = DateTimeOffset.Now,
            });

            if (result == SendMessageResult.Success) return ChatSendResult.Success;

            return ChatSendResult.FailByMessenger;
        }
    }
}
