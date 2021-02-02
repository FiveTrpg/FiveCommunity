using FiveCore.Community.Gameplay.Parties.Abstraction;
using FiveCore.Community.Gameplay.Parties.Abstraction.Results;
using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Parties
{
    public class Party : IParty
    {
        public List<IPartyMember> Members { get; set; } = new List<IPartyMember>();

        public string Identity { get; set; }
        public IPartyMember Owner { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } = string.Empty;
        public int MaxPlayer { get; set; }
        public bool Ended { get; set; }

        public event Action<IParty> OnClosed;
        public event Action<IParty, IPartyMember> OnJoined;
        public event Action<IParty, IPartyMember> OnLeaved;
        public event Action<IParty, IPartyMember> OnKicked;
        public event Action<IParty, IPartyMember, IPartyMember> OnTransforOwner;


        public PartyJoinResult Join(IPartyMember member, string password = "")
        {
            if (Ended) return PartyJoinResult.PartyEnded;
            if (Password != string.Empty && Password != password) return PartyJoinResult.PasswordIncorrect;
            if (Members.Count > MaxPlayer) return PartyJoinResult.PartyFull;
            if (Members.Contains(member)) return PartyJoinResult.PlayerAlreadyJoin;

            Members.Add(member);
            OnJoined?.Invoke(this, member);
            return PartyJoinResult.Success;
        }

        public PartyLeaveResult Leave(IPartyMember member)
        {
            if (!Members.Contains(member)) return PartyLeaveResult.PlayerNotInParty;

            Members.Remove(member);
            OnLeaved?.Invoke(this, member);
            return PartyLeaveResult.Success;
        }

        public PartyKickResult Kick(IPartyMember @operator, IPartyMember aimMember)
        {
            if (@operator != Owner) return PartyKickResult.Deny;
            if (!Members.Contains(aimMember)) return PartyKickResult.PlayerNotInParty;
            if (@operator == aimMember) return PartyKickResult.CantKickThyself;

            Members.Remove(aimMember);
            OnKicked?.Invoke(this, aimMember);
            return PartyKickResult.Success;
        }

        public PartyTransformResult TransforOwner(IPartyMember from, IPartyMember to)
        {
            if (Owner != from) return PartyTransformResult.Deny;

            Owner = to;
            OnTransforOwner?.Invoke(this, from, to);
            return PartyTransformResult.Success;
        }

        public PartyCloseResult Close(IPartyMember @operator)
        {
            if (Ended) return PartyCloseResult.PartyAlreadyEnd;
            if (@operator != Owner) return PartyCloseResult.Deny;

            Ended = true;
            OnClosed?.Invoke(this);
            return PartyCloseResult.Success;
        }

    }
}
