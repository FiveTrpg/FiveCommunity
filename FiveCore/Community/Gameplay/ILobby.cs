using FiveCore.Community.Gameplay.Parties;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay
{
    public interface ILobby
    {
        public IParty Party { get; set; }
        public List<IParty> Parties { get; set; }
        public Dictionary<IPlayer, IParty> PlayerLocation { get; set; }

        public PartyJoinResult Login(IPlayer player);

        public void OnPartyCreated(IParty party);

    }
}
