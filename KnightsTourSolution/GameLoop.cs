using System;

namespace KnightsTourSolution
{
    public class GameLoop
    {
        public enum gameStatus {CONTINUE, QUIT }
        public static gameStatus status = gameStatus.CONTINUE;
        public static void PlayGame()
        {
            SuperKnight gameSuperKnight = new SuperKnight(4, 7);
            ChessBoard gameChessBoard = new ChessBoard();

            do
            {
                if (SuperKnight.moveCounter != 0 && SuperKnight.moveCounter % 10 == 0)
                {
                    Console.Clear();
                }

                gameChessBoard.DisplayChessBoard();
                Console.WriteLine("The knight has executed {0} moves", SuperKnight.moveCounter);
                Console.WriteLine("\n\nThe knight is currently at ({0}, {1}.)", SuperKnight.X, SuperKnight.Y);

                gameSuperKnight.GetAvailableMoves(SuperKnight.X, SuperKnight.Y);
                gameSuperKnight.MoveKnight(gameSuperKnight.GetRecommendedMoves(gameSuperKnight.availableMoves, SuperKnight.X, SuperKnight.Y));
            }
            while (status == gameStatus.CONTINUE);
        }
    }
}
