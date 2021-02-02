namespace FiveCore.Community.Gameplay.Characters.Npcs
{
    public interface INpc : IUnique
    {
        public NpcType NpcType { get; }
    }
}
