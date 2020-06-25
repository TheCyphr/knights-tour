using System;

namespace ImprovedKnightsTourSolution.Models
{
    public class MoveAccessiblity : IComparable
    {
        public Move Move { get; set; }
        public int Accesibility { get; set; }

        public MoveAccessiblity(Move move, int accessibility)
        {
            this.Move = move;
            this.Accesibility = accessibility;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is MoveAccessiblity otherMoveAccessibility)
                return Accesibility.CompareTo(otherMoveAccessibility.Accesibility);
            else
                throw new ArgumentException("Object is not a MoveAccessiblity");
        }
    }
}
