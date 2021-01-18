using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay.Parties
{
    public partial class Party : IParty
    {
        public List<IPlayer> Players { get; set; } = new List<IPlayer>();

        public string Identity { get; set; }
        public IPlayer Owner { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } = string.Empty;
        public int MaxPlayer { get; set; }
        public bool Ended { get; set; }

        public event Action<IParty> OnClosed;
        public event Action<IParty, IPlayer> OnJoined;
        public event Action<IParty, IPlayer> OnLeaved;
        public event Action<IParty, IPlayer> OnKicked;
        public event Action<IParty, IPlayer, IPlayer> OnTransforOwner;

        public PartyJoinResult Join(IPlayer player, string password = "")
        {
            if (Ended) return PartyJoinResult.PartyEnded;
            if (Password != string.Empty && Password != password) return PartyJoinResult.PasswordIncorrect;
            if (Players.Count > MaxPlayer) return PartyJoinResult.PartyFull;
            if (Players.Contains(player)) return PartyJoinResult.PlayerAlreadyJoin;

            Players.Add(player);
            OnJoined?.Invoke(this, player);
            return PartyJoinResult.Success;
        }

        public PartyLeaveResult Leave(IPlayer player)
        {
            if (!Players.Contains(player)) return PartyLeaveResult.PlayerNotInParty;

            Players.Remove(player);
            OnLeaved?.Invoke(this, player);
            return PartyLeaveResult.Success;
        }

        public PartyKickResult Kick(IPlayer @operator, IPlayer aimPlayer)
        {
            if (@operator != Owner) return PartyKickResult.Deny;
            if (!Players.Contains(aimPlayer)) return PartyKickResult.PlayerNotInParty;
            if (@operator == aimPlayer) return PartyKickResult.CantKickThyself;

            Players.Remove(aimPlayer);
            OnKicked?.Invoke(this, aimPlayer);
            return PartyKickResult.Success;
        }

        public PartyTransformResult TransforOwner(IPlayer from, IPlayer to)
        {
            if (Owner != from) return PartyTransformResult.Deny;

            Owner = to;
            OnTransforOwner?.Invoke(this, from, to);
            return PartyTransformResult.Success;
        }

        public PartyCloseResult Close(IPlayer @operator)
        {
            if (Ended) return PartyCloseResult.PartyAlreadyEnd;
            if (@operator != Owner) return PartyCloseResult.Deny;

            Ended = true;
            OnClosed?.Invoke(this);
            return PartyCloseResult.Success;
        }

    }
}
