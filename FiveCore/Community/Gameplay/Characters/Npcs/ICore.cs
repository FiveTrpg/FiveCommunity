using FiveCore.Community.Gameplay.Parties.Abstraction;

namespace FiveCore.Community.Gameplay.Characters.Npcs
{
    public interface ICore : INpc, IPartyMember
    {
        public void CreateLobbyParty()
        {
            CreateParty("Lobby", "", int.MaxValue);
            Lobby.Party = CurrentParty;
        }
    }
}
