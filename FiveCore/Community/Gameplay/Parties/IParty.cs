using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Parties
{
    public interface IParty
    {
        public List<IPlayer> Players { get; set; }

        public string Identity { get; set; }
        public IPlayer Owner { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int MaxPlayer { get; set; }
        public bool Ended { get; set; }

        public event Action<IParty> OnClosed;
        public event Action<IParty, IPlayer> OnJoined;
        public event Action<IParty, IPlayer> OnLeaved;
        public event Action<IParty, IPlayer> OnKicked;
        public event Action<IParty, IPlayer, IPlayer> OnTransforOwner;

        public PartyJoinResult Join(IPlayer player, string password);
        public PartyLeaveResult Leave(IPlayer player);
        public PartyKickResult Kick(IPlayer @operator, IPlayer aimPlayer);
        public PartyTransformResult TransforOwner(IPlayer from, IPlayer to);
        public PartyCloseResult Close(IPlayer @operator);
    }
}
