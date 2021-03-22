using System;
using Curso_TDD.TicTacToe;
using NUnit.Framework;

namespace Curso_TDD
{
    [TestFixture]
    public class TicTacToeTests
    {
        [Test]
        public void CreateGame_GameIsInCorrectState()
        {
            TicTacToeGame game = new TicTacToeGame();

            Assert.AreEqual(0, game.MovesCounter);
            Assert.AreEqual(State.Unset, game.GetState(1));
        }

        [Test]
        public void MakeMove_CounterShifts()
        {
            TicTacToeGame game = new TicTacToeGame();
            game.MakeMove(1);

            Assert.AreEqual(1, game.MovesCounter);
        }

        [Test]
        public void MakeInvalidMove_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                TicTacToeGame game = new TicTacToeGame();
                game.MakeMove(0);

            });
        }

        [Test]
        public void MoveOnTheSameSquare_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => {
                TicTacToeGame game = new TicTacToeGame();
                game.MakeMove(1);
                game.MakeMove(1);
            });
        }

        [Test]
        public void MakingMoves_SetStateCorrectly()
        {
            TicTacToeGame game = new TicTacToeGame();

            game.MakeMove(1);
            game.MakeMove(2);
            game.MakeMove(3);
            game.MakeMove(4);

            Assert.AreEqual(State.Cross, game.GetState(1));
            Assert.AreEqual(State.Zero, game.GetState(2));
            Assert.AreEqual(State.Cross, game.GetState(3));
            Assert.AreEqual(State.Zero, game.GetState(4));
        }

        [Test]
        public void GetWinner_ZeroesWinVertically_ReturnsZeroes()
        {
            TicTacToeGame game = new TicTacToeGame();

            game.MakeMove(1);
            game.MakeMove(2);
            game.MakeMove(3);
            game.MakeMove(5);
            game.MakeMove(7);
            game.MakeMove(8);

            Assert.AreEqual(Winner.Zeroes, game.GetWinner());
        }

        [Test]
        public void GetWinner_CrossesWinDiagonal_ReturnsCrosses()
        {
            TicTacToeGame game = new TicTacToeGame();

            game.MakeMove(1);
            game.MakeMove(4);
            game.MakeMove(5);
            game.MakeMove(2);
            game.MakeMove(9);
            
            Assert.AreEqual(Winner.Crosses, game.GetWinner());
        }

        [Test]
        public void GetWinner_GameIsUnfinished_ReturnsGameIsUnfinished()
        {
            TicTacToeGame game = new TicTacToeGame();

            game.MakeMove(1);
            game.MakeMove(2);
            game.MakeMove(4);


            Assert.AreEqual(Winner.GameIsUnfinished, game.GetWinner());
        }

    }
}
