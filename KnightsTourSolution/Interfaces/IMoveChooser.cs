using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;
using System.Linq;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IMoveChooser
    {
        Move ChooseMove(int[,] chessboard, IChessPiece chessPiece);
    }

    public class MoveChooser : IMoveChooser
    {
        public Move ChooseMove(int[,] chessboard, IChessPiece chessPiece)
        {
            var lowestMoveAccessibilityGroup =
                chessPiece.Moves
                .Select(move => GetMoveRank(move, chessPiece.Point))
                .Where(mA => mA != null)
                .GroupBy(mA => mA.Rank)
                .OrderBy(mAGroup => mAGroup.Key)
                .FirstOrDefault();

            if (lowestMoveAccessibilityGroup?.Count() == 1)
            {
                return lowestMoveAccessibilityGroup.FirstOrDefault()?.Move;
            }

            var lowestMoveAccessibility = lowestMoveAccessibilityGroup?.Min(moveRank =>
            {
                var pointAfterMove = moveRank.Move.Execute(chessPiece.Point);
                // Set new point square to non default value, so that accessibility does not pick up one extra available move 
                chessboard[pointAfterMove.Y, pointAfterMove.X] = 10;

                moveRank.Rank = chessPiece.Moves.Min(move => GetMoveRank(move, pointAfterMove)).Rank;
                // Reset new point square value
                chessboard[pointAfterMove.Y, pointAfterMove.X] = 0;
                return moveRank;
            });

            return lowestMoveAccessibility?.Move;

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
                if (!ChessboardUtilities.OnChessboard(pointAfterMove, chessboard) || chessboard[pointAfterMove.Y, pointAfterMove.X] != 0) return null;

                var moveRank = new MoveRank(move, GetValidMoveCount(pointAfterMove, chessPiece.Moves));
                // We want to ignore moves that will end the game, until the move is the very last move of the game
                if (!IsLastSquareOnChessboard(chessboard) && moveRank.Rank == 0) return null;

                return moveRank;
            }
            
            int GetValidMoveCount(Point point, Move[] moves, int defaultSquareValue = 0)
            {
                return moves.Where(move =>
                {
                    var pointAfterMove = move.Execute(point);
                    var pointIsOnChessboard = ChessboardUtilities.OnChessboard(pointAfterMove, chessboard);

                    return pointIsOnChessboard && chessboard[pointAfterMove.Y, pointAfterMove.X] == defaultSquareValue;
                }).Count();
            }
        }
    }
}
