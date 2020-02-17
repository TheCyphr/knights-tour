namespace ImprovedKnightsTourSolution.Models
{
    public class Move
    {
        public int XMove { get; set; }
        public int YMove { get; set; }

        public Move(int xMove, int yMove)
        {
            XMove = xMove;
            YMove = yMove;
        }
    }
}
