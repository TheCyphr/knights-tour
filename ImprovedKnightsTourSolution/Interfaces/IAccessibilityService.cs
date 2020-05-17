using System.Linq;
using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IAccessibilityService
    {
        int[,] GetAccessibility(int[,] chessBoard, Move[] possibleMoves, int defaultSquareValue = 0);
    }

    public class AccessibilityService : IAccessibilityService
    {
        public int[,] GetAccessibility(int[,] chessBoard, Move[] possibleMoves, int defaultSquareValue = 0)
        {
            var accessibility = new int[chessBoard.GetLength(0), chessBoard.GetLength(1)];

            for (int row = 0; row < chessBoard.GetLength(0); row++)
            {
                for (int column = 0; column < chessBoard.GetLength(1); column++)
                {
                    accessibility[row, column] = GetNumberOfAvailableMovesAtSquare(new Point(column, row), possibleMoves, chessBoard, defaultSquareValue);
                }
            }

            return accessibility;
        }

        private int GetNumberOfAvailableMovesAtSquare(Point square, Move[] possibleMoves, int[,] chessboard, int defaultSquareValue)
        {
            return possibleMoves.Where(move =>
            {
                var newPoint = move.MovePoint(square);
                var pointIsOnChessboard = ChessboardUtilities.PointIsOnChessboard(newPoint, chessboard);
                return pointIsOnChessboard && chessboard[newPoint.Y, newPoint.X] == defaultSquareValue;
            }).Count();
        }
    }
}
