using ImprovedKnightsTourSolution.Models;

namespace ImprovedKnightsTourSolution.Utilities
{
    public static class ChessboardUtilities
    {
        public static bool OnChessboard(Point point, int[,] chessboard)
        {
            return point switch
            {
                var (x, y) when x < 0 || y < 0 => false,
                var (x, y) when x > chessboard.GetLength(1) - 1 || y > chessboard.GetLength(0) - 1 => false,
                _ => true
            };
        }
    }
}
