using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Parties;
using NUnit.Framework;
using System;

namespace FiveCore.Test.Community.Gameplay
{
    public class LobbyTest
    {
        private IPlayer player { get; set; }
        private ILobby lobby { get; set; }

        [SetUp]
        public void Setup()
        {
            player = new Player()
            {
                Identity = Guid.NewGuid().ToString(),
                Name = "Remilia",
            };

            lobby = new Lobby();
            Assert.AreEqual(lobby.Login(player), PartyJoinResult.Success);
        }

        [Test]
        public void PlayerJoinLobby()
        {
            Assert.IsTrue(lobby.PlayerLocation.ContainsKey(player));
            Assert.AreEqual(Lobby.Party, lobby.PlayerLocation[player]);
        }

        [Test]
        public void PlayerCreatePartyWithArguments()
        {
            var name = "Party";
            var password = "passw@rd";
            var maxPlayer = 19;
            Assert.AreEqual(PartyCreateResult.Success, player.Create(name, password, maxPlayer));
            Assert.AreEqual(1, lobby.Parties.Count);
            var party = lobby.Parties[0];
            Assert.AreEqual(party, lobby.PlayerLocation[player]);
            Assert.AreEqual(name, party.Name);
            Assert.AreEqual(password, party.Password);
            Assert.AreEqual(maxPlayer, party.MaxPlayer);
            Assert.AreEqual(player, party.Owner);
        }
    }
}
