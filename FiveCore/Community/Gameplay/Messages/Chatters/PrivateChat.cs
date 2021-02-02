using System;

namespace FiveCore.Community.Gameplay.Messages.Chatters
{
    public struct PrivateChat
    {
        public IUniqueChatter From { get; set; }
        public IUniqueChatter To { get; set; }
        public IMessage Message { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }
}
