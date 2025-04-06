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
    }

    public class KnightMover : IKnightMover
    {
        Move[] IKnightMover.Moves { get; } =
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
    }
}
