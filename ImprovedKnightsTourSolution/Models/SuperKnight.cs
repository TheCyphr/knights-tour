using System.Linq;

namespace ImprovedKnightsTourSolution.Models
{
    public class SuperKnight
    {
        public Point CurrentPosition { get; set; }
        
        private readonly Move[] AvailableMoves =
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

        public SuperKnight(Point currentPosition)
        {
            CurrentPosition = currentPosition;    
        }

        public bool IsValidMove(Move move) => AvailableMoves.Contains(move);
    }
}
