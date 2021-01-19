using FiveCore.Community.Gameplay.Npcs;
using FiveCore.Community.Gameplay.Parties;
using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay
{
    public class Lobby : ILobby
    {
        public List<IParty> Parties { get; set; } = new List<IParty>();
        public Dictionary<IPlayer, IParty> PlayerLocation { get; set; } = new Dictionary<IPlayer, IParty>();
        public Dictionary<string, IPlayer> Players { get; set; } = new Dictionary<string, IPlayer>();

        public event Action<IPlayer, IParty> OnPartyCreated;
        public static Party Party => Party.Lobby;

        public Lobby()
        {
            Party.Factory.OnPartyCreated += Factory_OnPartyCreated;
            RegisterEventToParty(Lobby.Party);
            Lobby.Party.Join(Core.Instance);
        }

        public void RegisterEventToParty(IParty party)
        {
            party.OnJoined += Lobby_OnJoined;
            party.OnClosed += Party_OnClosed;
        }

        private void Party_OnClosed(IParty obj) => Parties.Remove(obj);

        private void Factory_OnPartyCreated(IParty party)
        {
            if (party.Owner is IPlayer player)
            {
                OnPartyCreated?.Invoke(player, party);
                RegisterEventToParty(party);
                Parties.Add(party);
            }
        }

        private void Lobby_OnJoined(IParty party, IPartyMember member)
        {
            if (member is IPlayer player)
                PlayerLocation[player] = party;
        }

        public PartyJoinResult Login(IPlayer player)
        {
            if (!Players.ContainsKey(player.Identity)) Players.Add(player.Identity, player);

            return player.JoinParty(Party.Lobby);
        }
    }
}
