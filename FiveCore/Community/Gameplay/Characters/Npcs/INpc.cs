namespace FiveCore.Community.Gameplay.Characters.Npcs
{
    public interface INpc : IUniqued
    {
        public NpcType NpcType { get; }
    }
}
