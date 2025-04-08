using System;
using KnightsTourSolution.Interfaces;
using KnightsTourSolution.Models;

namespace KnightsTourSolution
{
    class Program
    {
        static void Main()
        {
            SolveForEveryStartingPoint();
            Console.WriteLine("Solved!");
        }

        private static void SolveForEveryStartingPoint()
        {
            IMoveChooser moveChooser = new MoveChooser();
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessboard chessboard = new Chessboard();
                    IKnightsTourSimulator knightsTourSimulator = new KnightsTourSimulator(chessboard, moveChooser);
                    bool isSolved = knightsTourSimulator.Solve(new Knight {Point = new Point(i, j), Id = 1});
                    if (!isSolved)
                    {
                        throw new Exception($"Solution failed with starting point ({i}, {j})");
                    }
                }
            }
        }
    }
}
