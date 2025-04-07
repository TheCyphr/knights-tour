using ImprovedKnightsTourSolution.Models;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IChessboardService
    {
        public int[,] Chessboard { get; }
        public int DefaultSquareValue { get; }
        public bool TryPlace(IChessPiece piece);
        public bool OnChessboard(Point point);
    }

    public class ChessboardService : IChessboardService
    {
        public int[,] Chessboard { get; } = new int[8, 8];
        public int DefaultSquareValue => 0;

        public bool TryPlace(IChessPiece piece)
        {
            var onChessboard = OnChessboard(piece.Point);

            if (onChessboard)
            {
                Chessboard[piece.Point.Y, piece.Point.X] = piece.Id;
            }

            return onChessboard;
        }
        
        public bool OnChessboard(Point point)
        {
            return point switch
            {
                var (x, y) when x < 0 || y < 0 => false,
                var (x, y) when x > Chessboard.GetLength(1) - 1 || y > Chessboard.GetLength(0) - 1 => false,
                _ => true
            };
        }
    }
}
