using System;

namespace KnightsTourSolution.Interfaces
{
    public interface IKnightsTourSimulator
    {
        bool Solve(IChessPiece chessPiece);
    }

    public class KnightsTourSimulator : IKnightsTourSimulator
    {
        private readonly IChessboard _chessboard;
        private readonly IMoveChooser _moveChooser;

        public KnightsTourSimulator(IChessboard chessboard, IMoveChooser moveChooser)
        {
            _chessboard = chessboard;
            _moveChooser = moveChooser;
        }

        public bool Solve(IChessPiece chessPiece)
        {
            if (!_chessboard.TryPlace(chessPiece))
            {
                throw new Exception("Starting point is invalid");
            }
            
            while (true)
            {
                var move = _moveChooser.Choose(_chessboard, chessPiece);
                if (move == null)
                {
                    return _chessboard.EmptySquareCount == 0;
                }

                chessPiece.Point = move.Execute(chessPiece.Point);
                if (!_chessboard.TryPlace(chessPiece))
                {
                    throw new Exception("Move invalid");
                }
            }
        }
    }
}
