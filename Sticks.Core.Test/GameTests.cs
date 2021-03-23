using System;
using NUnit.Framework;

namespace Sticks.Core.Test
{
    [TestFixture]
    public class GameTests
    {

        [Test]
        public void Ctor_LessThen10Sticks_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Game(9, Player.Machine));
        }

        [Test]
        public void Ctor_ValidaParams_GameIsCorrectState()
        {
            int sticks = 10;
            Player player = Player.Machine;

            var sut = new Game(sticks, player);

            Assert.That(sut.NumberOfSticks, Is.EqualTo(sticks));
            Assert.That(sut.Turn, Is.EqualTo(player));
        }

        [TestCase(0)]
        [TestCase(4)]
        public void HumanMakesMove_InvalidNumberOfSticks_Throws(int take)
        {
            var sut = new Game(10, Player.Human);
            Assert.Throws<ArgumentException>(() => sut.HumanMakesMove(take));
        }

        [TestCase(1, 9)]
        [TestCase(2, 8)]
        [TestCase(3, 7)]
        public void HumanMakesMove_CorrectGameState(int takes, int remain)
        {
            var sut = new Game(10, Player.Human);
            sut = sut.HumanMakesMove(takes);

            Assert.That(sut.NumberOfSticks, Is.EqualTo(remain));
            Assert.That(sut.Turn, Is.EqualTo(Player.Machine));


        }

        [Test]
        public void HumanMakesMove_TurnOfMachine_Throws()
        {
            var sut = new Game(10, Player.Machine);

            Assert.Throws<InvalidOperationException>(() => sut.HumanMakesMove(1));
        }

        [TestCase(1, 9)]
        [TestCase(2, 8)]
        [TestCase(3, 7)]
        public void MachineMakesMove_CorrectyGameState(int takes)
        {
            var gen = new PredictableGenerator();
            gen.SetNumber(takes);

            var sut = new Game(10, Player.Machine, gen);
        }

    }

    public class PredictableGenerator : ICanGenerateNumbers
    {
        private int _number;

        public int Next(int min, int max) {

            return _number;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }
    }
}
