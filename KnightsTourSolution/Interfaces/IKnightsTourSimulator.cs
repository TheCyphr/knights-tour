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
        private readonly IKnightMover _knightMover;

        public KnightsTourSimulator(IChessboardService chessboardService, IMoveChooser moveChooser, IKnightMover knightMover)
        {
            _chessboardService = chessboardService;
            _moveChooser = moveChooser;
            _knightMover = knightMover;
        }

        public bool Solve(Point startingPoint)
        {
            var knight = new Knight {Point = startingPoint};
            const char knightIdentifier = 'K';

            _chessboardService.TrySetSquareValue(knight.Point, knightIdentifier);
            
            int moveCount = 0;
            
            while (true)
            {
                var bestMove = _moveChooser.ChooseMove(_chessboardService.Chessboard, _knightMover.Moves, knight.Point);

                if (bestMove == null)
                {
                    return moveCount == 63;
                }

                if (_chessboardService.TrySetSquareValue(bestMove.MovePoint(knight.Point), knightIdentifier))
                {
                    knight.Point = bestMove.MovePoint(knight.Point);
                }
                else
                    throw new Exception("Move invalid");
                
                moveCount++;
            }
        }
    }
}
