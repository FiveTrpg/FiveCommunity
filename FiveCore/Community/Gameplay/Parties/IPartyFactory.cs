namespace FiveCore.Community.Gameplay.Parties
{
    public interface IPartyFactory
    {
        public PartyCreateResult Create(IPlayer player, string name, string password, int maxPalyer, out IParty party);
    }
}
