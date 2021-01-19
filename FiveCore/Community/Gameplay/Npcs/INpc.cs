namespace FiveCore.Community.Gameplay.Npcs
{
    public interface INpc : IUniqued
    {
        public NpcType NpcType { get; }
    }
}
