using ImprovedKnightsTourSolution.Models;
using System;

namespace ImprovedKnightsTourSolution.Interfaces
{
    /// <summary>
    /// Controls the game based on input from the player
    /// </summary>
    public interface IGameController
    {
        bool PlayGame(Point startingPoint, int consoleClearRate = 10, bool isEnabledWaitForUserInput = true);
    }

    public class GameController : IGameController
    {
        private readonly IChessboardService chessboardService;
        private readonly IMoveChooser moveChooser;
        private readonly IKnightMover knightMover;

        public GameController(IAccessibilityService accessibilityService, IChessboardService chessboardService, IMoveChooser moveChooser, IKnightMover knightMover)
        {
            moveChooser.AccessibilityService = accessibilityService;

            this.chessboardService = chessboardService;
            this.moveChooser = moveChooser;
            this.knightMover = knightMover;
        }

        public bool PlayGame(Point startingPoint, int consoleClearRate = 10, bool isEnabledWaitForUserInput = true)
        {
            // Set knight starting position
            var superKnight = new SuperKnight(startingPoint);
            var knightIdentifier = 'K';

            chessboardService.TrySetSquareValue(superKnight.CurrentPosition, knightIdentifier);
            chessboardService.DisplayChessboard();
            
            int moveCount = 0;
            bool sentinelValue = !isEnabledWaitForUserInput || Console.ReadKey() != null;

            while (sentinelValue)
            {
                if (moveCount % consoleClearRate == 0)
                {
                    Console.Clear();
                }

                var bestMove = moveChooser.ChooseMoveWithLowestAccessibility(chessboardService.Chessboard, knightMover.Moves, superKnight.CurrentPosition);

                if (bestMove == null)
                {
                    bool isGameWon = moveCount == 63;
                    var endGameText = isGameWon ? "You Win! Congratulations for doing absolutely nothing!" : "You Lose! You gota practice more!";
                    
                    Console.WriteLine($"No moves left. Knight executed {moveCount} moves");
                    Console.WriteLine(endGameText);
                    
                    return isGameWon;
                }

                if (chessboardService.TrySetSquareValue(bestMove.MovePoint(superKnight.CurrentPosition), knightIdentifier))
                {
                    superKnight.CurrentPosition = bestMove.MovePoint(superKnight.CurrentPosition);
                }
                else
                    throw new Exception("Move invalid");

                Console.WriteLine();
                chessboardService.DisplayChessboard();
                moveCount++;
            }

            return false;
        }
    }
}
