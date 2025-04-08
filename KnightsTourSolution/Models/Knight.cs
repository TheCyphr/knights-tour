using KnightsTourSolution.Interfaces;

namespace KnightsTourSolution.Models
{
    public class Knight : IChessPiece
    {
        public int Id { get; set; }
        public Point Point { get; set; }
        public Move[] Moves { get; } =
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
