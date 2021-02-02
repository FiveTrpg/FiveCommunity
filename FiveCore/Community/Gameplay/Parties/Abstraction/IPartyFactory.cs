using FiveCore.Community.Gameplay.Parties.Abstraction.Results;
using System;

namespace FiveCore.Community.Gameplay.Parties.Abstraction
{
    public interface IPartyFactory
    {
        public event Action<IParty> OnPartyCreated;
        public PartyCreateResult Create(IPartyMember player, string name, string password, int maxPalyer, out IParty party);
    }
}
