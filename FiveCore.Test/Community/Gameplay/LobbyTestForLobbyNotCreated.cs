using Autofac;
using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Characters;
using FiveCore.Community.Gameplay.Characters.Npcs;
using FiveCore.Community.Gameplay.Parties;
using FiveCore.Community.Utilities;
using NUnit.Framework;
using System;

namespace FiveCore.Test.Community.Gameplay
{
    public class LobbyTestForLobbyNotCreated
    {
        private IPlayer player { get; set; }
        private ILobby lobby { get; set; }
        private IContainer game { get; set; }
        private ICore core { get; set; }

        [SetUp]
        public void Setup()
        {
            game = GameplayContainer.Game();
            lobby = game.Resolve<ILobby>();

            player = game.Resolve<IPlayer>();
            player.Identity = Guid.NewGuid().ToString();
            player.Name = "Remilia";

            core = game.Resolve<ICore>();
            Assert.That(core.NpcType, Is.EqualTo(NpcType.Observer));
        }

        [Test]
        public void PlayerCanJoinWhenLobbyPartyCreated()
        {
            core.CreateLobbyParty();
            Assert.AreEqual(lobby.Login(player), PartyJoinResult.Success);
        }

        [Test]
        public void PlayerCantJoinWhenLobbyPartyCreated()
        {
            Assert.AreEqual(lobby.Login(player), PartyJoinResult.PartyNotExist);
        }
    }
}
