namespace FiveCore.Community.Gameplay.Parties.Abstraction.Results
{
    public enum PartyJoinResult
    {
        Success = 0,
        PartyFull = 1,
        PlayerAlreadyJoin = 2,
        PasswordIncorrect = 3,
        PartyEnded = 4,
        PartyNotExist = 5,
    }
}
