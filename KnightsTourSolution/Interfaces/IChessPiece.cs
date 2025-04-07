using ImprovedKnightsTourSolution.Models;

namespace ImprovedKnightsTourSolution.Interfaces
{
    public interface IChessPiece
    {
        int Id { get; set; }
        Move[] Moves { get; }
        Point Point { get; set; }
    }
}