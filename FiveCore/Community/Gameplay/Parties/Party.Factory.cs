using FiveCore.Community.Gameplay.Npcs;
using System;

namespace FiveCore.Community.Gameplay.Parties
{
    public partial class Party
    {
        public static Party Lobby { get; } = new Party()
        {
            Name = "Lobby",
            Identity = "Lobby",
            MaxPlayer = int.MaxValue,
            Owner = Core.Instance,
        };

        public static PartyFactory Factory { get; } = new PartyFactory();
        public class PartyFactory : IPartyFactory
        {
            public event Action<IParty> OnPartyCreated;

            public PartyCreateResult Create(IPartyMember player, string name, string password, int maxPalyer, out IParty party)
            {
                party = null;
                if (!Lobby.Members.Contains(player)) return PartyCreateResult.CreatorNotInLobby;
                party = new Party()
                {
                    Identity = Guid.NewGuid().ToString(),
                    Name = name,
                    Password = password,
                    MaxPlayer = maxPalyer,
                    Owner = player,
                };
                OnPartyCreated?.Invoke(party);
                return PartyCreateResult.Success;
            }
        }
    }
}
