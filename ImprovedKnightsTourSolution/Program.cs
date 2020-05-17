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
            IChessboardService chessboardService = new ChessboardService();
            IAccessibilityService accessibilityService = new AccessibilityService();
            chessboardService.TrySetSquareValue(superKnight.CurrentPosition, 'K');
            var accessibility = accessibilityService.GetAccessibility(chessboardService.Chessboard, knightMover.Moves);
            Console.WriteLine(chessboardService);
            PrintAccessibility(accessibility);
        }

        static void PrintAccessibility(int[,] accessibility)
        {
            for (int i = 0; i < accessibility.GetLength(0); i++)
            {
                for (int j = 0; j < accessibility.GetLength(1); j++)
                {
                    Console.Write(accessibility[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
