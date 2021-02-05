using FiveCore.Community.Gameplay.Characters;
using System;

namespace FiveCore.Community.Gameplay.Messages.Abstraction.Chatters
{
    public interface IUniqueChatter : IUnique
    {
        public event Action<PrivateChat> OnReceive;
        public void SendMessage(PrivateChat message);
    }
}
