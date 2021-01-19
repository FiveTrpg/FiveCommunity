using FiveCore.Community.Gameplay.Parties;

namespace FiveCore.Community.Gameplay
{
    public class Player : IPlayer
    {
        public string Identity { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Age { get; set; }
        public string Location { get; set; }
        public IParty CurrentParty { get; set; }

        private ILobby Lobby { get; }
        private IPartyFactory PartyFactory { get; }

        ILobby IPartyMember.Lobby => Lobby;
        IPartyFactory IPartyMember.PartyFactory => PartyFactory;

        public Player(ILobby lobby, IPartyFactory partyFactory)
        {
            Lobby = lobby;
            PartyFactory = partyFactory;
        }
    }
}
