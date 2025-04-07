namespace ImprovedKnightsTourSolution.Models
{
    public class Move
    {
        public int X { get; }
        public int Y { get; }

        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Execute(Point point) => new Point(point.X + X, point.Y + Y);
    }
}
