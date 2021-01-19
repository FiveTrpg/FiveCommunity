using Autofac;
using FiveCore.Community.Gameplay;
using FiveCore.Community.Gameplay.Npcs;
using FiveCore.Community.Gameplay.Parties;
using NUnit.Framework;
using System;

namespace FiveCore.Test.Community.Gameplay
{
    public class PartyTest : GameplayEnviroment
    {
        private IPlayer remilia { get; set; }
        private IPlayer flandre { get; set; }
        private ILobby lobby { get; set; }
        private IContainer game { get; set; }
        private ICore core { get; set; }

        private string name = "Party";
        private string password = "passw@rd";
        private int maxPlayer = 19;

        [SetUp]
        public void Setup()
        {
            SetupEnviroment();
            game = Build();
            core = game.Resolve<ICore>();
            core.CreateLobbyParty();
            remilia = game.Resolve<IPlayer>();
            remilia.Identity = Guid.NewGuid().ToString();
            remilia.Name = "Remilia";
            remilia.Gender = "♂";
            remilia.Race = "吸血鬼";
            remilia.Age = "Unknown";
            remilia.Location = "Gensokyo";

            flandre = game.Resolve<IPlayer>();
            flandre.Identity = Guid.NewGuid().ToString();
            flandre.Name = "Flandre";

            lobby = game.Resolve<ILobby>();
            Assert.AreEqual(lobby.Login(remilia), PartyJoinResult.Success);
            Assert.AreEqual(PartyCreateResult.Success, remilia.CreateParty(name, password, maxPlayer));
        }

        [Test]
        public void PlayerJoinPartyWithIncorrectPassword()
        {
            var party = lobby.PlayerLocation[remilia];

            Assert.That(party.Members, Has.No.One.EqualTo(flandre));
            Assert.That(flandre.JoinParty(party), Is.EqualTo(PartyJoinResult.PasswordIncorrect));
            Assert.That(party.Members, Has.No.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerJoinPartyWithCorrectPassword()
        {
            var party = lobby.PlayerLocation[remilia];

            Assert.That(party.Members, Has.No.One.EqualTo(flandre));
            Assert.That(flandre.JoinParty(party, password), Is.EqualTo(PartyJoinResult.Success));
            Assert.That(party.Members, Has.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerLeaveParty()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = flandre.CurrentParty;
            Assert.That(party.Members, Has.One.EqualTo(flandre));
            Assert.That(flandre.LeaveParty(), Is.EqualTo(PartyLeaveResult.Success));
            Assert.That(party.Members, Has.No.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerKickOtherPlayer()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = remilia.CurrentParty;

            Assert.That(party.Members, Has.One.EqualTo(flandre));
            Assert.That(party.Kick(@operator: remilia, aimPlayer: flandre), Is.EqualTo(PartyKickResult.Success));
            Assert.That(party.Members, Has.No.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerKickSelf()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = remilia.CurrentParty;

            Assert.That(party.Members, Has.One.EqualTo(flandre));
            Assert.That(party.Kick(@operator: remilia, aimPlayer: remilia), Is.EqualTo(PartyKickResult.CantKickThyself));
            Assert.That(party.Members, Has.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerKickWhenNotIsOwner()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = remilia.CurrentParty;

            Assert.That(party.Members, Has.One.EqualTo(remilia));
            Assert.That(party.Owner, Is.Not.EqualTo(flandre));
            Assert.That(party.Kick(@operator: flandre, aimPlayer: remilia), Is.EqualTo(PartyKickResult.Deny));
            Assert.That(party.Members, Has.One.EqualTo(remilia));
        }

        [Test]
        public void PlayerTransformOwner()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = remilia.CurrentParty;

            Assert.That(party.Owner, Is.EqualTo(remilia));
            Assert.That(party.TransforOwner(remilia, flandre), Is.EqualTo(PartyTransformResult.Success));
            Assert.That(party.Owner, Is.EqualTo(flandre));
        }

        [Test]
        public void PlayerTransformOwnerWhenNotIsOwner()
        {
            PlayerJoinPartyWithCorrectPassword();
            var party = remilia.CurrentParty;

            Assert.That(party.Owner, Is.Not.EqualTo(flandre));
            Assert.That(party.TransforOwner(flandre, remilia), Is.EqualTo(PartyTransformResult.Deny));
            Assert.That(party.Owner, Is.Not.EqualTo(flandre));
        }

        [Test]
        public void PlayerCloseParty()
        {
            var party = remilia.CurrentParty;

            Assert.That(party.Ended, Is.Not.True);
            Assert.That(party.Close(remilia), Is.EqualTo(PartyCloseResult.Success));
            Assert.That(party.Ended, Is.True);
        }

        [Test]
        public void PlayerJoinAClosedParty()
        {
            var party = remilia.CurrentParty;
            PlayerCloseParty();
            Assert.That(flandre.JoinParty(party, password), Is.EqualTo(PartyJoinResult.PartyEnded));
            Assert.That(party.Members, Has.No.One.EqualTo(flandre));
        }

        [Test]
        public void PlayerCloseAClosedParty()
        {
            var party = remilia.CurrentParty;
            PlayerCloseParty();

            Assert.That(party.Close(remilia), Is.EqualTo(PartyCloseResult.PartyAlreadyEnd));
            Assert.That(party.Ended, Is.True);
        }

        [Test]
        public void PlayerCloseAPartyWhenNotIsOwner()
        {
            var party = remilia.CurrentParty;

            Assert.That(party.Ended, Is.Not.True);
            Assert.That(party.Owner, Is.Not.EqualTo(flandre));
            Assert.That(party.Close(flandre), Is.EqualTo(PartyCloseResult.Deny));
            Assert.That(party.Ended, Is.Not.True);
        }

        [Test]
        public void CoreCanJoiEveryParty()
        {
            Assert.That(((IPartyMember)core).JoinParty(remilia.CurrentParty, remilia.CurrentParty.Password),
                Is.EqualTo(PartyJoinResult.Success));
        }
    }
}
