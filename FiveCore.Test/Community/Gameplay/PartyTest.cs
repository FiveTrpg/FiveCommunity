using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Parties;
using NUnit.Framework;
using System;

namespace FiveCore.Test.Community.Gameplay
{
    public class PartyTest
    {
        private IPlayer remilia { get; set; }
        private IPlayer flandre { get; set; }
        private ILobby lobby { get; set; }

        private string name = "Party";
        private string password = "passw@rd";
        private int maxPlayer = 19;

        [SetUp]
        public void Setup()
        {
            remilia = new Player()
            {
                Identity = Guid.NewGuid().ToString(),
                Name = "Remilia",
            };
            flandre = new Player()
            {
                Identity = Guid.NewGuid().ToString(),
                Name = "Flandre",
            };

            lobby = new Lobby();
            Assert.AreEqual(lobby.Login(remilia), PartyJoinResult.Success);
            Assert.AreEqual(PartyCreateResult.Success, remilia.Create(name, password, maxPlayer));
        }

        [Test]
        public void PlayerJoinPartyWithIncorrectPassword()
        {
            var party = lobby.PlayerLocation[remilia];
            Assert.That(flandre.JoinParty(party), Is.EqualTo(PartyJoinResult.PasswordIncorrect));
            Assert.That(party.Players, Has.No.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerJoinPartyWithCorrectPassword()
        {
            var party = lobby.PlayerLocation[remilia];
            Assert.That(flandre.JoinParty(party, password), Is.EqualTo(PartyJoinResult.Success));
            Assert.That(party.Players, Has.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerLeaveParty()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = flandre.CurrentParty;
            Assert.That(flandre.Leave(), Is.EqualTo(PartyLeaveResult.Success));
            Assert.That(party.Players, Has.No.One.EqualTo(flandre));
        }
    }
}
