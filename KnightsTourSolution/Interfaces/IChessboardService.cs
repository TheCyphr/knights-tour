using ImprovedKnightsTourSolution.Utilities;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IChessboardService
    {
        public int[,] Chessboard { get; }
        public bool TryPlace(IChessPiece piece);
    }

    public class ChessboardService : IChessboardService
    {
        public int[,] Chessboard { get; } = new int[8, 8];

        public bool TryPlace(IChessPiece piece)
        {
            var onChessboard = ChessboardUtilities.OnChessboard(piece.Point, Chessboard);

            if (onChessboard)
            {
                Chessboard[piece.Point.Y, piece.Point.X] = piece.Id;
            }

            return onChessboard;
        }
    }
}
