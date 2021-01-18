using System;

namespace FiveCore.Community.Gameplay
{
    public partial class Party
    {
        public static Party Lobby { get; } = new Party()
        {
            Name = "Lobby",
            Identity = "Lobby",
        };

        public static PartyFactory Factory { get; } = new PartyFactory();
        public class PartyFactory : IPartyFactory
        {
            public event Action<IParty> OnPartyCreated;

            public PartyCreateResult Create(IPlayer player, string name, string password, int maxPalyer, out IParty party)
            {
                party = null;
                if (!Lobby.Players.Contains(player)) return PartyCreateResult.CreatorNotInLobby;
                party = new Party()
                {
                    Identity = Guid.NewGuid().ToString(),
                    Name = name,
                    Password = password,
                    MaxPlayer = maxPalyer,
                };
                return PartyCreateResult.Success;
            }
        }
    }
}
