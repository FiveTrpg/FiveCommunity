using FiveCore.Community.Gameplay.Parties;

namespace FiveCore.Community.Gameplay.Npcs
{
    public class Core : ICore
    {
        public string Identity { get; set; } = "0134F9C9-AB14-4B76-9675-035EEE98FD8A";
        public string Name { get; set; } = "Core (System)";
        public IParty CurrentParty { get; set; }

        public NpcType NpcType => NpcType.Observer;

        private ILobby Lobby { get; }
        private IPartyFactory PartyFactory { get; }
        ILobby IPartyMember.Lobby => Lobby;
        IPartyFactory IPartyMember.PartyFactory => PartyFactory;

        public Core(ILobby lobby, IPartyFactory partyFactory)
        {
            Lobby = lobby;
            PartyFactory = partyFactory;

        }
    }
}
