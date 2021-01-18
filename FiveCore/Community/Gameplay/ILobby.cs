﻿using FiveCore.Community.Gameplay.Parties;
using System;
using System.Collections.Generic;

namespace FiveCore.Community.Gameplay
{
    public interface ILobby
    {
        public List<IParty> Parties { get; set; }
        public Dictionary<IPlayer, IParty> PlayerLocation { get; set; }

        public PartyJoinResult Login(IPlayer player);

        public event Action<IPlayer, IParty> OnPartyCreated;

    }
}
