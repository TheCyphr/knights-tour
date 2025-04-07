using ImprovedKnightsTourSolution.Models;
using System;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IKnightsTourSimulator
    {
        bool Solve(Point startingPoint);
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

        public bool Solve(Point startingPoint)
        {
            const int maxMoveCount = 63;
            int moveCount = 0;
            IChessPiece knight = new Knight {Point = startingPoint, Id = 1};
            _chessboardService.TryPlace(knight);
            
            while (true)
            {
                var move = _moveChooser.ChooseMove(_chessboardService.Chessboard, knight);
                if (move == null)
                {
                    return moveCount == maxMoveCount;
                }

                knight.Point = move.Execute(knight.Point);
                if (!_chessboardService.TryPlace(knight))
                {
                    throw new Exception("Move invalid");
                }
                moveCount++;
            }
        }
    }
}
