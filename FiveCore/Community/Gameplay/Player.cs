﻿using FiveCore.Community.Gameplay.Parties;

namespace FiveCore.Community.Gameplay
{
    public class Player : IPlayer
    {
        public string Identity { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Age { get; set; }
        public string Location { get; set; }
        public IParty CurrentParty { get; set; }

        public PartyCreateResult Create(string name, string password, int maxPalyer)
        {
            var result = Party.Factory.Create(this, name, password, maxPalyer, out var party);
            if (result == PartyCreateResult.Success) CurrentParty = party;
            JoinParty(party, password);
            return result;
        }

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
    }
}
