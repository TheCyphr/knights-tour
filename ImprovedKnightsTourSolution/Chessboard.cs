using ImprovedKnightsTourSolution.Models;
using System.Collections.Generic;
using System.Text;

namespace ImprovedKnightsTourSolution
{
    public class Chessboard
    {
        private readonly int[,] _chessBoard;
        private readonly Dictionary<int, char> _chessboardSquareIdentifiers;
        private const char DEFAULT_IDENTIFIER = '0';

        public Chessboard()
        {
            _chessBoard = new int[8, 8];
            _chessboardSquareIdentifiers = new Dictionary<int, char>() { { 0, DEFAULT_IDENTIFIER } };
        }

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

            var isOnChessboard = point switch
            {
                var (x, y) when x < 0 || y < 0 => false,
                var (x, y) when x > _chessBoard.GetLength(1) || y > _chessBoard.GetLength(0) => false,
                _ => true
            };

            if (isOnChessboard)
            {
                _chessBoard[point.Y, point.X] = identifierIndex;
            }

            return isOnChessboard;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int row = 0; row < _chessBoard.GetLength(0); row++)
            {
                for (int column = 0; column < _chessBoard.GetLength(1); column++)
                {
                    char squareCharacter = _chessboardSquareIdentifiers[_chessBoard[row, column]];
                    stringBuilder.Append(squareCharacter);
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
