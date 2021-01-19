namespace FiveCore.Community.Gameplay.Parties
{
    public interface IPartyMember
    {
        public IParty CurrentParty { get; set; }

        public PartyJoinResult JoinParty(IParty party, string password = "")
        {
            var result = party.Join(this, password);
            if (result == PartyJoinResult.Success)
            {
                CurrentParty = party;
            }
            return result;
        }

        public PartyLeaveResult Leave()
        {
            if (CurrentParty == Party.Lobby) return PartyLeaveResult.PlayerNotInParty;

            return CurrentParty.Leave(this);
        }

        public PartyCreateResult Create(string name, string password, int maxPalyer)
        {
            var result = Party.Factory.Create(this, name, password, maxPalyer, out var party);
            if (result == PartyCreateResult.Success) CurrentParty = party;
            JoinParty(party, password);
            return result;
        }
    }
}
