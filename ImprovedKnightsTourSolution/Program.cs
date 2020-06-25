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
                    IAccessibilityService accessibilityService = new AccessibilityService();
                    IMoveChooser moveChooser = new MoveChooser(accessibilityService);
                    IGameController gameController = new GameController(accessibilityService, chessboardService, moveChooser, knightMover);

                    bool isGameWon = gameController.PlayGame(new Point(i, j), isEnabledWaitForUserInput: false);
                    if (!isGameWon) throw new Exception($"Lost game at starting point ({i}, {j})");
                }
            }

            Console.Clear();
            Console.WriteLine("Game is won from all starting points, good job!");
        }
    }
}
