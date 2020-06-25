using System;
using ImprovedKnightsTourSolution.Interfaces;
using ImprovedKnightsTourSolution.Models;

namespace ImprovedKnightsTourSolution
{
    class Program
    {
        static void Main()
        {
            IKnightMover knightMover = new KnightMover();
            IChessboardService chessboardService = new ChessboardService();
            IAccessibilityService accessibilityService = new AccessibilityService();
            IMoveChooser moveChooser = new MoveChooser(accessibilityService);
            IGameController gameController = new GameController(accessibilityService, chessboardService, moveChooser, knightMover);

            gameController.PlayGame(new Point(4, 2), isEnabledWaitForUserInput: false);
        }
    }
}
