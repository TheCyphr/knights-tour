using KnightsTourSolution.Models;

namespace KnightsTourSolution.Interfaces
{
    public interface IChessPiece
    {
        int Id { get; set; }
        Move[] Moves { get; }
        Point Point { get; set; }
    }
}