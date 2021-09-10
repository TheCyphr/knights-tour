using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;
using System.Linq;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IMoveChooser
    {
        public IAccessibilityService AccessibilityService { get; set; }
        Move ChooseMoveWithLowestAccessibility(int[,] accessibility, Move[] possibleMoves, Point currentPosition);
    }

    public class MoveChooser : IMoveChooser
    {
        public IAccessibilityService AccessibilityService { get; set; }

        public MoveChooser(IAccessibilityService accessibilityService)
        {
            this.AccessibilityService = accessibilityService;
        }

        public Move ChooseMoveWithLowestAccessibility(int[,] chessboard, Move[] possibleMoves, Point currentPosition)
        {
            bool IsLastSquareOnChessboard(int[,] board, Point point)
            {
                int availableSquareCount = 0;
                
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int column = 0; column < board.GetLength(1); column++)
                    {
                        if (board[column, row] == 0) availableSquareCount++;
                    }
                }

                return availableSquareCount == 1;
            }

            MoveAccessiblity GetMoveAccessiblity(Move move, Point point)
            {
                var pointAfterMove = move.MovePoint(point);
                if (!ChessboardUtilities.PointIsOnChessboard(pointAfterMove, chessboard) || chessboard[pointAfterMove.Y, pointAfterMove.X] != 0) return null;

                var moveAccessbility = new MoveAccessiblity(move, AccessibilityService.GetNumberOfAvailableMovesAtPoint(pointAfterMove, possibleMoves, chessboard));
                // We want to ignore moves that will end the game, until the move is the very last move of the game
                if (!IsLastSquareOnChessboard(chessboard, pointAfterMove) && moveAccessbility.Accesibility == 0) return null;

                return moveAccessbility;
            }

            var lowestMoveAccessiblityGroup =
                possibleMoves
                .Select(move => GetMoveAccessiblity(move, currentPosition))
                .Where(mA => mA != null)
                .GroupBy(mA => mA.Accesibility)
                .OrderBy(mAGroup => mAGroup.Key)
                .FirstOrDefault();

            if (lowestMoveAccessiblityGroup?.Count() == 1)
            {
                return lowestMoveAccessiblityGroup.FirstOrDefault()?.Move;
            }

            var lowestMoveAccessibility = lowestMoveAccessiblityGroup?.Min(mA =>
            {
                var pointAfterMove = mA.Move.MovePoint(currentPosition);
                // Set new point square to non default value, so that accessibility does not pick up one extra available move 
                chessboard[pointAfterMove.Y, pointAfterMove.X] = 10;

                mA.Accesibility = possibleMoves.Min(move => GetMoveAccessiblity(move, pointAfterMove)).Accesibility;
                // Reset new point square value
                chessboard[pointAfterMove.Y, pointAfterMove.X] = 0;
                return mA;
            });

            return lowestMoveAccessibility?.Move;
        }
    }
}
