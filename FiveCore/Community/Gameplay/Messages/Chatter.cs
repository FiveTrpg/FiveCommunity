using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Messages.Channels;
using System;

namespace FiveCore.Community.Gameplay.Messages
{
    public class Chatter : IChannelChatter
    {
        public IUnique Unique { get; set; }
        public bool Muted { get; set; }
        public IChannel Channel { get; set; }

        public event Action<ChannelChat> OnReceive;

        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
