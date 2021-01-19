using FiveCore.Community.Gameplay.Parties;

namespace FiveCore.Community.Gameplay
{
    public interface IPlayer : IUniqued, IPartyMember
    {
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Age { get; set; }
        public string Location { get; set; }
    }
}
