using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Parties
{
    public interface IParty
    {
        public List<IPartyMember> Members { get; set; }

        public string Identity { get; set; }
        public IPartyMember Owner { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int MaxPlayer { get; set; }
        public bool Ended { get; set; }

        public event Action<IParty> OnClosed;
        public event Action<IParty, IPartyMember> OnJoined;
        public event Action<IParty, IPartyMember> OnLeaved;
        public event Action<IParty, IPartyMember> OnKicked;
        public event Action<IParty, IPartyMember, IPartyMember> OnTransforOwner;

        public PartyJoinResult Join(IPartyMember player, string password);
        public PartyLeaveResult Leave(IPartyMember player);
        public PartyKickResult Kick(IPartyMember @operator, IPartyMember aimPlayer);
        public PartyTransformResult TransforOwner(IPartyMember from, IPartyMember to);
        public PartyCloseResult Close(IPartyMember @operator);
    }
}
