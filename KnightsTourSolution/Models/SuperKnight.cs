using System.Linq;

namespace ImprovedKnightsTourSolution.Models
{
    public class SuperKnight
    {
        public Point CurrentPosition { get; set; }

        public SuperKnight(Point currentPosition)
        {
            CurrentPosition = currentPosition;    
        }
    }
}
