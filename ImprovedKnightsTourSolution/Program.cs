using System;
using ImprovedKnightsTourSolution.Interfaces;
using ImprovedKnightsTourSolution.Models;

namespace ImprovedKnightsTourSolution
{
    class Program
    {
        static void Main()
        {
            var superKnight = new SuperKnight(new Point(5, 5));
            IKnightMover knightMover = new KnightMover();
            knightMover.TryMoveKnight(superKnight, knightMover.Moves[0]);
            var chessboard = new Chessboard();
            chessboard.TrySetSquareValue(superKnight.CurrentPosition, 'K');
            Console.WriteLine(chessboard);
        }
    }
}
