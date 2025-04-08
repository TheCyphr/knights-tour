using KnightsTourSolution.Models;

namespace KnightsTourSolution.Interfaces
{
    public interface IChessboard
    {
        public int EmptySquareCount { get; }
        public bool TryPlace(IChessPiece piece);
        public bool CanMoveTo(Point point);
    }

    public class Chessboard : IChessboard
    {
        private readonly int[,] _chessboard = new int[Length, Length];
        public int EmptySquareCount { get; private set; } = Length * Length;
        private const int Length = 8;
        private static int DefaultSquareValue => 0;

        public bool TryPlace(IChessPiece piece)
        {
            var onChessboard = OnChessboard(piece.Point);

            if (onChessboard)
            {
                _chessboard[piece.Point.Y, piece.Point.X] = piece.Id;
                EmptySquareCount--;
            }

            return onChessboard;
        }
        
        private bool OnChessboard(Point point)
        {
            return point switch
            {
                var (x, y) when x < 0 || y < 0 => false,
                var (x, y) when x > _chessboard.GetLength(1) - 1 || y > _chessboard.GetLength(0) - 1 => false,
                _ => true
            };
        }
        
        public bool CanMoveTo(Point point)
        {
            return OnChessboard(point) && 
                   _chessboard[point.Y, point.X] == DefaultSquareValue;
        }
    }
}
