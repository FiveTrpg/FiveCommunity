namespace FiveCore.Community.Gameplay
{
    public interface IPlayer
    {
        public string Identity { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Age { get; set; }
        public string Location { get; set; }

        public IParty CurrentParty { get; set; }

        public PartyJoinResult JoinParty(IParty party, string password = "");
        public PartyLeaveResult Leave();
        public PartyCreateResult Create(string name, string password, int maxPalyer);
    }
}
