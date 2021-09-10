using System.Linq;
using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IAccessibilityService
    {
        int GetNumberOfAvailableMovesAtPoint(Point point, Move[] possibleMoves, int[,] chessboard, int defaultSquareValue = 0);
    }

    public class AccessibilityService : IAccessibilityService
    {
        public int GetNumberOfAvailableMovesAtPoint(Point point, Move[] possibleMoves, int[,] chessboard, int defaultSquareValue = 0)
        {
            return possibleMoves.Where(move =>
            {
                var pointAfterMove = move.MovePoint(point);
                var pointIsOnChessboard = ChessboardUtilities.PointIsOnChessboard(pointAfterMove, chessboard);

                return pointIsOnChessboard && chessboard[pointAfterMove.Y, pointAfterMove.X] == defaultSquareValue;
            }).Count();
        }
    }
}
