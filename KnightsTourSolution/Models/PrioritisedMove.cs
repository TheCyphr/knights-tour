using System;

namespace KnightsTourSolution.Models
{
    public class PrioritisedMove : IComparable
    {
        public Move Move { get; }
        // Lower value means higher priority
        public int ExecutePriority { get; set; }

        public PrioritisedMove(Move move, int executePriority)
        {
            Move = move;
            ExecutePriority = executePriority;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is PrioritisedMove moveRank)
                return ExecutePriority.CompareTo(moveRank.ExecutePriority);
            else
                throw new ArgumentException($"Object is not a {nameof(PrioritisedMove)}");
        }
    }
}
