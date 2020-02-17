using ImprovedKnightsTourSolution.Models;
using System;
using System.Linq;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IKnightMover
    {
        public Move[] Moves { get; }
        public bool IsValidMove(Move move)
        {
            if (move == null)
            {
                throw new ArgumentNullException(nameof(move));
            }

            return Moves.Contains(move);
        }
        public bool TryMoveKnight(SuperKnight superKnight, Move move);
    }

    public class KnightMover : IKnightMover
    {
        private readonly Move[] moves =
        {
            new Move(-1, 2),
            new Move(-2, 1),
            new Move(-2, -1),
            new Move(-1, -2),
            new Move(1, -2),
            new Move(2, -1),
            new Move(2, 1),
            new Move(1, 2)
        };

        Move[] IKnightMover.Moves => moves;

        bool IKnightMover.TryMoveKnight(SuperKnight superKnight, Move move)
        {
            if (((IKnightMover)this).IsValidMove(move))
            {
                var current = superKnight.CurrentPosition;
                var updated = new Point(current.X + move.XMove, current.Y + move.YMove);
                superKnight.CurrentPosition = updated;

                return true;
            }

            return false;
        }
    }
}
