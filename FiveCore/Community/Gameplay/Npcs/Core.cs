using FiveCore.Community.Gameplay.Parties;

namespace FiveCore.Community.Gameplay.Npcs
{
    public class Core : IUniqued, IPartyMember
    {
        public static readonly Core Instance = new Core();
        public string Identity { get; set; } = "0134F9C9-AB14-4B76-9675-035EEE98FD8A";
        public string Name { get; set; } = "Core (System)";
        public IParty CurrentParty { get; set; }
    }
}
