namespace FiveCore.Community.Gameplay.Parties
{
    public interface IPartyMember
    {
        public IParty CurrentParty { get; set; }
        protected ILobby Lobby { get; }
        protected IPartyFactory PartyFactory { get; }

        public PartyJoinResult JoinParty(IParty party, string password = "")
        {
            if (party == null) return PartyJoinResult.PartyNotExist;
            var result = party.Join(this, password);
            if (result == PartyJoinResult.Success)
            {
                CurrentParty = party;
            }
            return result;
        }

        public PartyLeaveResult LeaveParty()
        {
            if (CurrentParty == Lobby.Party) return PartyLeaveResult.PlayerNotInParty;

            return CurrentParty.Leave(this);
        }

        public PartyCreateResult CreateParty(string name, string password, int maxPalyer)
        {
            var result = PartyFactory.Create(this, name, password, maxPalyer, out var party);
            if (result == PartyCreateResult.CreatorNotInLobby) return result;
            if (CurrentParty != null && CurrentParty == Lobby.Party) CurrentParty.Leave(this);

            JoinParty(party, password);
            return result;
        }
    }
}
