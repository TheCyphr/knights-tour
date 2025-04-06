using System;
using ImprovedKnightsTourSolution.Interfaces;
using ImprovedKnightsTourSolution.Models;

namespace ImprovedKnightsTourSolution
{
    class Program
    {
        static void Main()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IKnightMover knightMover = new KnightMover();
                    IChessboardService chessboardService = new ChessboardService();
                    IMoveChooser moveChooser = new MoveChooser(new MoveRankService());
                    IKnightsTourSimulator knightsTourSimulator = new KnightsTourSimulator(chessboardService, moveChooser, knightMover);

                    bool isSolved = knightsTourSimulator.Solve(new Point(i, j));
                    if (!isSolved) throw new Exception($"Solution failed with starting point ({i}, {j})");
                }
            }
            
            Console.WriteLine("Solved!");
        }
    }
}
