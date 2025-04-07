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
            var bestMoveGroup =
                chessPiece.Moves
                .Select(move => GetMoveRank(move, chessPiece.Point))
                .Where(moveRank => moveRank != null)
                .GroupBy(moveRank => moveRank.Rank)
                .OrderBy(moveRanks => moveRanks.Key)
                .FirstOrDefault();

            if (bestMoveGroup == null) return null;
            
            if (bestMoveGroup.Count() == 1)
            {
                return bestMoveGroup.First().Move;
            }

            var bestMoveRank = bestMoveGroup.Min(moveRank =>
            {
                var pointAfterMove = moveRank.Move.Execute(chessPiece.Point);
                chessboardService.Chessboard[pointAfterMove.Y, pointAfterMove.X] = chessPiece.Id;

                moveRank.Rank = chessPiece.Moves.Min(move => GetMoveRank(move, pointAfterMove)).Rank;
                chessboardService.Chessboard[pointAfterMove.Y, pointAfterMove.X] = chessboardService.DefaultSquareValue;
                
                return moveRank;
            });

            return bestMoveRank?.Move;

            bool IsLastSquareOnChessboard(int[,] board)
            {
                int availableSquareCount = 0;
                
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int column = 0; column < board.GetLength(1); column++)
                    {
                        if (board[column, row] == 0) availableSquareCount++;
                    }
                }

                return availableSquareCount == 1;
            }

            MoveRank GetMoveRank(Move move, Point pointToMove)
            {
                var pointAfterMove = move.Execute(pointToMove);
                if (!IsValid(pointAfterMove)) return null;

                var moveRank = new MoveRank(move, chessPiece.Moves.Count(m => IsValid(m.Execute(pointAfterMove))));
                // Until the final move, ignore moves that do not allow further moves
                if (!IsLastSquareOnChessboard(chessboardService.Chessboard) && moveRank.Rank == 0) return null;

                return moveRank;
            }
            
            bool IsValid(Point point)
            {
                return chessboardService.OnChessboard(point) && 
                       chessboardService.Chessboard[point.Y, point.X] == chessboardService.DefaultSquareValue;
            }
        }
    }
}
