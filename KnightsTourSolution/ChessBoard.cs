using System;

namespace KnightsTourSolution
{
    public class ChessBoard
    {
        public static int[,] chessBoard;
        
        public ChessBoard()
        {
            chessBoard = new int[8, 8];
        }

        public void DisplayChessBoard()
        {
            for (int row = 0; row < chessBoard.GetLength(0); row++)
            {
                for (int column = 0; column < chessBoard.GetLength(1); column++)
                {
                    if (row == SuperKnight.X && column == SuperKnight.Y)
                    {
                        chessBoard[row, column] = 1;
                        Console.Write("K");
                    }
                    else
                    {
                        Console.Write("{0}", chessBoard[row, column]);
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
