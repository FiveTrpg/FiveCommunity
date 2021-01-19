using FiveCore.Community.Gameplay.Parties;

namespace FiveCore.Community.Gameplay.Npcs
{
    public interface INpc : IUniqued, IPartyMember
    {
        public NpcType NpcType { get; }
    }
}
