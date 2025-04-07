using System;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IKnightsTourSimulator
    {
        bool Solve(IChessPiece chessPiece);
    }

    public class KnightsTourSimulator : IKnightsTourSimulator
    {
        private readonly IChessboardService _chessboardService;
        private readonly IMoveChooser _moveChooser;

        public KnightsTourSimulator(IChessboardService chessboardService, IMoveChooser moveChooser)
        {
            _chessboardService = chessboardService;
            _moveChooser = moveChooser;
        }

        public bool Solve(IChessPiece chessPiece)
        {
            const int maxMoveCount = 63;
            int moveCount = 0;
            if (!_chessboardService.TryPlace(chessPiece))
            {
                throw new Exception("Starting point is invalid");
            }
            
            while (true)
            {
                var move = _moveChooser.Choose(_chessboardService, chessPiece);
                if (move == null)
                {
                    return moveCount == maxMoveCount;
                }

                chessPiece.Point = move.Execute(chessPiece.Point);
                if (!_chessboardService.TryPlace(chessPiece))
                {
                    throw new Exception("Move invalid");
                }
                moveCount++;
            }
        }
    }
}
