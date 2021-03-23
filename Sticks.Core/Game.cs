using System;
namespace Sticks.Core
{
    public class Game
    {
        private const int MinToTake = 1;
        private const int MaxToTake = 3;
        private object p;

        public int NumberOfSticks { get;}
        public Player Turn { get; }

        public Game(int numberOfSticks, Player turn)
        {

            NumberOfSticks = numberOfSticks;
            Turn = turn;

            if(numberOfSticks < 10)
            {
                throw new ArgumentException($"Num de stick debe ser mayor o iguala 10. Seteaste {numberOfSticks}");
            }

        }

        public Game(Player turn, int numberOfSticks)
        {
            NumberOfSticks = numberOfSticks;
            Turn = turn;
        }

        public Game HumanMakesMove(int stickTaken)
        {
            if(Turn == Player.Machine)
            {
                throw new InvalidOperationException();
            }

            if (stickTaken < MinToTake || stickTaken > MaxToTake)
            {
                throw new ArgumentException($"Debes tomar 1 o 3 sticks. Tomaste {stickTaken}");
            }
            return new Game(RevertPlayer(Turn), NumberOfSticks-stickTaken);
        }

        private Player RevertPlayer(Player turn)
        {
            return turn == Player.Machine ? Player.Human : Player.Machine;
        }
    }

    public interface ICanGenerateNumbers
    {
        int Next(int min, int max);
    }

    public class NumbersGenerator : ICanGenerateNumbers
    {
        private readonly Random _generator = new Random();

        public int Next(int min, int max)
        {
            return _generator.Next(min, max);
        }
    }
}
