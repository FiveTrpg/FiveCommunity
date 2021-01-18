using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay
{
    public class Lobby : ILobby
    {
        public List<IParty> Parties { get; set; }
        public Dictionary<IPlayer, IParty> PlayerLocation { get; set; }
        public Dictionary<string, IPlayer> Players { get; set; }

        public event Action<IPlayer, IParty> OnPartyCreated;

        public Lobby()
        {
            Party.Factory.OnPartyCreated += Factory_OnPartyCreated;
            RegisterEventToParty(Party.Lobby);

        }

        public void RegisterEventToParty(IParty party)
        {
            party.OnJoined += Lobby_OnJoined;
            party.OnClosed += Party_OnClosed;
        }

        private void Party_OnClosed(IParty obj) => Parties.Remove(obj);

        private void Factory_OnPartyCreated(IParty obj) => Parties.Add(obj);

        private void Lobby_OnJoined(IParty party, IPlayer player)
        {
            PlayerLocation[player] = party;
        }

        public PartyJoinResult Login(IPlayer player)
        {
            if (!Players.ContainsKey(player.Identity)) Players.Add(player.Identity, player);

            return player.JoinParty(Party.Lobby);
        }
    }
}
