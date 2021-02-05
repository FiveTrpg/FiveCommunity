using FiveCore.Community.Gameplay.Messages.Abstraction.Chatters;
using FiveCore.Community.Gameplay.Messages.Abstraction.Messengers.Results;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Messages.Abstraction.Messengers
{
    public interface IPrivateMessenger
    {
        public List<IUniqueChatter> RegisteredChatters { get; set; }

        public RegisterResult RegisterChatter(IUniqueChatter chatter);
        public SendMessageResult SendMessage(PrivateChat chat);
    }
}
