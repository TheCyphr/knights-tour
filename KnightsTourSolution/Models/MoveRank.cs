using System;

namespace ImprovedKnightsTourSolution.Models
{
    public class MoveRank : IComparable
    {
        public Move Move { get; }
        public int Rank { get; set; }

        public MoveRank(Move move, int rank)
        {
            Move = move;
            Rank = rank;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is MoveRank moveRank)
                return Rank.CompareTo(moveRank.Rank);
            else
                throw new ArgumentException($"Object is not a {nameof(MoveRank)}");
        }
    }
}
