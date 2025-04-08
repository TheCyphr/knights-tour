using System.Linq;
using KnightsTourSolution.Models;

namespace KnightsTourSolution.Interfaces
{
    public interface IMoveChooser
    {
        Move Choose(IChessboard chessboard, IChessPiece chessPiece);
    }

    public class MoveChooser : IMoveChooser
    {
        public Move Choose(IChessboard chessboard, IChessPiece chessPiece)
        {
            var highestPriorityMoves =
                chessPiece.Moves
                .Select(move => PrioritiseMove(move, chessPiece.Point))
                .Where(rankedMove => rankedMove != null)
                .GroupBy(rankedMove => rankedMove.ExecutePriority)
                .OrderBy(rankedMoves => rankedMoves.Key)
                .FirstOrDefault();

            if (highestPriorityMoves == null) return null;
            
            if (highestPriorityMoves.Count() == 1)
            {
                return highestPriorityMoves.First().Move;
            }

            var highestPriorityMove = highestPriorityMoves.Min(prioritisedMove =>
            {
                var movedPoint = prioritisedMove.Move.Execute(chessPiece.Point);
                prioritisedMove.ExecutePriority = chessPiece.Moves.Min(move => PrioritiseMove(move, movedPoint)).ExecutePriority;
                
                return prioritisedMove;
            });

            return highestPriorityMove?.Move;

            PrioritisedMove PrioritiseMove(Move move, Point point)
            {
                var pointAfterMove = move.Execute(point);
                if (!chessboard.CanMoveTo(pointAfterMove))
                {
                    return null;
                }

                // Determine execute priority based on number of available subsequent moves
                var subsequentMoveCount = chessPiece.Moves.Count(m => chessboard.CanMoveTo(m.Execute(pointAfterMove)));
                // Until the final move, ignore moves that have no subsequent moves
                if (chessboard.EmptySquareCount > 1 &&
                    subsequentMoveCount == 0)
                {
                    return null;
                }
                
                return new PrioritisedMove(move, subsequentMoveCount);
            }
        }
    }
}
