using FiveCore.Community.Gameplay.Parties;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay
{
    public class Lobby : ILobby
    {
        public List<IParty> Parties { get; set; } = new List<IParty>();
        public Dictionary<IPlayer, IParty> PlayerLocation { get; set; } = new Dictionary<IPlayer, IParty>();
        public Dictionary<string, IPlayer> Players { get; set; } = new Dictionary<string, IPlayer>();

        public IParty Party { get; set; }

        public void RegisterEventToParty(IParty party)
        {
            party.OnJoined += Lobby_OnJoined;
            party.OnClosed += Party_OnClosed;
            party.OnLeaved += Party_OnLeaved;
        }

        private void Party_OnLeaved(IParty party, IPartyMember member)
        {
            if (member is IPlayer)
            {
                if (party != Party)
                {
                    member.JoinParty(Party);
                }
            }
        }

        private void Party_OnClosed(IParty obj) => Parties.Remove(obj);

        public void OnPartyCreated(IParty party)
        {
            RegisterEventToParty(party);
            if (party.Owner is IPlayer player)
            {
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

            return player.JoinParty(Party);
        }
    }
}
