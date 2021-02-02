using Autofac;
using FiveCore.Community.Gameplay.Parties.Abstraction;
using FiveCore.Community.Gameplay.Parties.Abstraction.Results;
using System;

namespace FiveCore.Community.Gameplay.Parties
{
    public class PartyFactory : IPartyFactory
    {
        private ILifetimeScope Scope { get; }
        public ILobby Lobby { get; }
        public PartyFactory(ILifetimeScope scope)
        {
            Scope = scope;
            Lobby = Scope.Resolve<ILobby>();
            this.OnPartyCreated += Lobby.OnPartyCreated;
        }

        public event Action<IParty> OnPartyCreated;

        public PartyCreateResult Create(IPartyMember player, string name, string password, int maxPalyer, out IParty party)
        {
            party = null;
            if (Lobby.Party != null && !Lobby.Party.Members.Contains(player))
                return PartyCreateResult.CreatorNotInLobby;
            party = Scope.Resolve<IParty>();
            party.Identity = Guid.NewGuid().ToString();
            party.Name = name;
            party.Password = password;
            party.MaxPlayer = maxPalyer;
            party.Owner = player;
            OnPartyCreated?.Invoke(party);
            return PartyCreateResult.Success;
        }
    }
}
