using FiveCore.Community.Gameplay.Parties.Abstraction;

namespace FiveCore.Community.Gameplay.Characters
{
    public interface IPlayer : IUnique, IPartyMember
    {
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Age { get; set; }
        public string Location { get; set; }
    }
}
