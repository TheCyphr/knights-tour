using ImprovedKnightsTourSolution.Models;
using System.Linq;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IMoveChooser
    {
        Move Choose(IChessboardService chessboardService, IChessPiece chessPiece);
    }

    public class MoveChooser : IMoveChooser
    {
        public Move Choose(IChessboardService chessboardService, IChessPiece chessPiece)
        {
            var lowestRankedMoves =
                chessPiece.Moves
                .Select(move => RankMove(move, chessPiece.Point))
                .Where(rankedMove => rankedMove != null)
                .GroupBy(rankedMove => rankedMove.Rank)
                .OrderBy(rankedMoves => rankedMoves.Key)
                .FirstOrDefault();

            if (lowestRankedMoves == null) return null;
            
            if (lowestRankedMoves.Count() == 1)
            {
                return lowestRankedMoves.First().Move;
            }

            var lowestRankedMove = lowestRankedMoves.Min(rankedMove =>
            {
                var movedPoint = rankedMove.Move.Execute(chessPiece.Point);
                chessboardService.Chessboard[movedPoint.Y, movedPoint.X] = chessPiece.Id;

                rankedMove.Rank = chessPiece.Moves.Min(move => RankMove(move, movedPoint)).Rank;
                chessboardService.Chessboard[movedPoint.Y, movedPoint.X] = chessboardService.DefaultSquareValue;
                
                return rankedMove;
            });

            return lowestRankedMove?.Move;

            bool IsLastSquareOnChessboard(int[,] board)
            {
                int availableSquareCount = 0;
                
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int column = 0; column < board.GetLength(1); column++)
                    {
                        if (board[column, row] == 0 && ++availableSquareCount > 1) return false;
                    }
                }

                return true;
            }

            RankedMove RankMove(Move move, Point pointToMove)
            {
                var pointAfterMove = move.Execute(pointToMove);
                if (!IsValid(pointAfterMove)) return null;

                var rankedMove = new RankedMove(move, chessPiece.Moves.Count(m => IsValid(m.Execute(pointAfterMove))));
                // Until the final move, ignore moves that do not allow further moves
                if (!IsLastSquareOnChessboard(chessboardService.Chessboard) && rankedMove.Rank == 0) return null;

                return rankedMove;
            }
            
            bool IsValid(Point point)
            {
                return chessboardService.OnChessboard(point) && 
                       chessboardService.Chessboard[point.Y, point.X] == chessboardService.DefaultSquareValue;
            }
        }
    }
}
