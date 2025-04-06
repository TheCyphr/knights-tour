using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;
using System.Collections.Generic;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IChessboardService
    {
        public int[,] Chessboard { get; }
        public bool TrySetSquareValue(Point point, char identifier);
    }

    public class ChessboardService : IChessboardService
    {
        public int[,] Chessboard { get; } = new int[8, 8];
        private readonly Dictionary<int, char> _chessboardSquareIdentifiers = new Dictionary<int, char> { { 0, DEFAULT_IDENTIFIER } };
        private const char DEFAULT_IDENTIFIER = '0';

        private int AssignIdentifierIndex(char identifier)
        {
            int identifierIndex = _chessboardSquareIdentifiers.Count;
            
            foreach (var (key, value) in _chessboardSquareIdentifiers)
            {
                if (value == identifier)
                {
                    identifierIndex = key;
                }
            }
            
            _chessboardSquareIdentifiers[identifierIndex] = identifier;
            return identifierIndex;
        }

        public bool TrySetSquareValue(Point point, char identifier)
        {
            var identifierIndex = AssignIdentifierIndex(identifier);

            var onChessboard = ChessboardUtilities.OnChessboard(point, Chessboard);

            if (onChessboard)
            {
                Chessboard[point.Y, point.X] = identifierIndex;
            }

            return onChessboard;
        }
    }
}
