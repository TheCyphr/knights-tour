using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IChessboardService
    {
        public int[,] Chessboard { get; }
        public bool TrySetSquareValue(Point point, char identifier);
        public void DisplayChessboard();
    }

    public class ChessboardService : IChessboardService
    {
        public int[,] Chessboard { get; }
        private readonly Dictionary<int, char> _chessboardSquareIdentifiers;
        private const char DEFAULT_IDENTIFIER = '0';

        public ChessboardService()
        {
            Chessboard = new int[8, 8];
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

            var pointIsOnChessboard = ChessboardUtilities.PointIsOnChessboard(point, Chessboard);

            if (pointIsOnChessboard)
            {
                Chessboard[point.Y, point.X] = identifierIndex;
            }

            return pointIsOnChessboard;
        }

        public void DisplayChessboard()
        {
            var stringBuilder = new StringBuilder();

            for (int row = 0; row < Chessboard.GetLength(0); row++)
            {
                for (int column = 0; column < Chessboard.GetLength(1); column++)
                {
                    char squareCharacter = _chessboardSquareIdentifiers[Chessboard[row, column]];
                    stringBuilder.Append(squareCharacter);
                }

                stringBuilder.AppendLine();
            }

            Console.Write(stringBuilder.ToString());
        }
    }
}
