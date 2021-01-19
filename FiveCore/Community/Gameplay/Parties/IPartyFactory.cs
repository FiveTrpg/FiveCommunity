using System;

namespace FiveCore.Community.Gameplay.Parties
{
    public interface IPartyFactory
    {
        public event Action<IParty> OnPartyCreated;
        public PartyCreateResult Create(IPartyMember player, string name, string password, int maxPalyer, out IParty party);
    }
}
