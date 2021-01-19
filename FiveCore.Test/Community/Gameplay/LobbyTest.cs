using Autofac;
using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Npcs;
using FiveCore.Community.Gameplay.Parties;
using NUnit.Framework;
using System;

namespace FiveCore.Test.Community.Gameplay
{
    public class LobbyTest : GameplayEnviroment
    {
        private IPlayer player { get; set; }
        private ILobby lobby { get; set; }
        private IContainer game { get; set; }
        private Core core { get; set; }

        [SetUp]
        public void Setup()
        {
            SetupEnviroment();
            game = Build();
            lobby = game.Resolve<ILobby>();

            player = game.Resolve<IPlayer>();
            player.Identity = Guid.NewGuid().ToString();
            player.Name = "Remilia";

            core = game.Resolve<Core>();

            Assert.AreEqual(lobby.Login(player), PartyJoinResult.Success);
        }

        [Test]
        public void LobbyOwnerIsSystem()
        {
            Assert.That(core.CurrentParty, Is.EqualTo(lobby.Party));
            Assert.That(lobby.Party.Owner, Is.EqualTo(core));
            Assert.That(lobby.Party.Members, Has.One.EqualTo(core));
        }

        [Test]
        public void PlayerJoinLobby()
        {
            Assert.IsTrue(lobby.PlayerLocation.ContainsKey(player));
            Assert.AreEqual(lobby.Party, lobby.PlayerLocation[player]);
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

        [Test]
        public void PlayerCreatePartyWhenNotInLobby()
        {
            PlayerCreatePartyWithArguments();
            var name = "Party2";
            var password = "passw@rd";
            var maxPlayer = 19;
            Assert.AreEqual(PartyCreateResult.CreatorNotInLobby, player.Create(name, password, maxPlayer));
            Assert.AreEqual(1, lobby.Parties.Count);
        }
    }
}
