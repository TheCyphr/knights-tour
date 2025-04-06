using System.Linq;
using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IMoveRankService
    {
        int GetAvailableMoveCount(Point point, Move[] possibleMoves, int[,] chessboard, int defaultSquareValue = 0)
        {
            return possibleMoves.Where(move =>
            {
                var pointAfterMove = move.MovePoint(point);
                var pointIsOnChessboard = ChessboardUtilities.OnChessboard(pointAfterMove, chessboard);

                return pointIsOnChessboard && chessboard[pointAfterMove.Y, pointAfterMove.X] == defaultSquareValue;
            }).Count();
        }
    }

    public class MoveRankService : IMoveRankService
    {
        
    }
}
