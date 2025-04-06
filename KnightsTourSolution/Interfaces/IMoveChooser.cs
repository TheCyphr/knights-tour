using ImprovedKnightsTourSolution.Models;
using ImprovedKnightsTourSolution.Utilities;
using System.Linq;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IMoveChooser
    {
        Move ChooseMove(int[,] chessboard, Move[] possibleMoves, Point point);
    }

    public class MoveChooser : IMoveChooser
    {
        public IMoveRankService MoveRankService { get; set; }

        public MoveChooser(IMoveRankService moveRankService)
        {
            this.MoveRankService = moveRankService;
        }

        public Move ChooseMove(int[,] chessboard, Move[] possibleMoves, Point point)
        {
            var lowestMoveAccessibilityGroup =
                possibleMoves
                .Select(move => GetMoveRank(move, point))
                .Where(mA => mA != null)
                .GroupBy(mA => mA.Rank)
                .OrderBy(mAGroup => mAGroup.Key)
                .FirstOrDefault();

            if (lowestMoveAccessibilityGroup?.Count() == 1)
            {
                return lowestMoveAccessibilityGroup.FirstOrDefault()?.Move;
            }

            var lowestMoveAccessibility = lowestMoveAccessibilityGroup?.Min(mA =>
            {
                var pointAfterMove = mA.Move.MovePoint(point);
                // Set new point square to non default value, so that accessibility does not pick up one extra available move 
                chessboard[pointAfterMove.Y, pointAfterMove.X] = 10;

                mA.Rank = possibleMoves.Min(move => GetMoveRank(move, pointAfterMove)).Rank;
                // Reset new point square value
                chessboard[pointAfterMove.Y, pointAfterMove.X] = 0;
                return mA;
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
                var pointAfterMove = move.MovePoint(pointToMove);
                if (!ChessboardUtilities.OnChessboard(pointAfterMove, chessboard) || chessboard[pointAfterMove.Y, pointAfterMove.X] != 0) return null;

                var moveAccessibility = new MoveRank(move, MoveRankService.GetAvailableMoveCount(pointAfterMove, possibleMoves, chessboard));
                // We want to ignore moves that will end the game, until the move is the very last move of the game
                if (!IsLastSquareOnChessboard(chessboard) && moveAccessibility.Rank == 0) return null;

                return moveAccessibility;
            }
        }
    }
}
