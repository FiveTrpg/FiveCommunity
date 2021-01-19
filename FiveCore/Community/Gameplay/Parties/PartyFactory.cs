using System;

namespace FiveCore.Community.Gameplay.Parties
{
    public class PartyFactory : IPartyFactory
    {
        public ILobby Lobby { get; }
        public PartyFactory(ILobby lobby)
        {
            Lobby = lobby;
            this.OnPartyCreated += lobby.OnPartyCreated;
        }

        public event Action<IParty> OnPartyCreated;

        public PartyCreateResult Create(IPartyMember player, string name, string password, int maxPalyer, out IParty party)
        {
            party = null;
            if (Lobby.Party != null && !Lobby.Party.Members.Contains(player))
                return PartyCreateResult.CreatorNotInLobby;
            party = new Party()
            {
                Identity = Guid.NewGuid().ToString(),
                Name = name,
                Password = password,
                MaxPlayer = maxPalyer,
                Owner = player,
            };
            OnPartyCreated?.Invoke(party);
            return PartyCreateResult.Success;
        }
    }
}
