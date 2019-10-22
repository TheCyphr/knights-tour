using System;
using System.Collections.Generic;

namespace KnightsTourSolution
{
    class SuperKnight
    {
        public List<int> availableMoves = new List<int>();
        public static int moveCounter;
        public static int X { get; set; }
        public static int Y { get; set; }
        public int[] moveX = { -1, -2, -2, -1, 1, 2, 2, 1 };
        public int[] moveY = { 2, 1, -1, -2, -2, -1, 1, 2 };
        public int[,] accessibility = { {2, 3, 4, 4, 4, 4, 3, 2 }, {3, 4, 6, 6, 6, 6, 4, 3 }, {4, 6, 8, 8, 8, 8, 6, 4 }, { 4, 6, 8, 8, 8, 8, 6, 4 },
                                                   {4, 6, 8, 8, 8, 8, 6, 4 }, {4, 6, 8, 8, 8, 8, 6, 4 }, {3, 4, 6, 6, 6, 6, 4, 3 }, { 2, 3, 4, 4, 4, 4, 3, 2 } };

        public SuperKnight(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveKnight(List<int> moves)
        {
            int[] recommendedMoves = moves.ToArray();
            int moveToExecute = 10;

            if (recommendedMoves.Length > 1)
            {
                int bestMove = 8;

                for (int i = 0; i < recommendedMoves.Length; i++)
                {
                    int tempX = X + (moveX[recommendedMoves[i]]);
                    int tempY = Y + (moveY[recommendedMoves[i]]);

                    GetAvailableMoves(tempX, tempY);
                    int nextMoveChoices = GetRecommendedMoves(availableMoves, tempX, tempY).Count;

                    if ( nextMoveChoices < bestMove && nextMoveChoices != 0)
                    {
                        bestMove = nextMoveChoices;
                    }
                }
                for (int i = 0; i < recommendedMoves.Length; i++)
                {
                    int tempX = X + (moveX[recommendedMoves[i]]);
                    int tempY = Y + (moveY[recommendedMoves[i]]);

                    GetAvailableMoves(tempX, tempY);
                    int nextMoveChoices = GetRecommendedMoves(availableMoves, tempX, tempY).Count;

                    if (nextMoveChoices == bestMove)
                    {
                        moveToExecute = recommendedMoves[i];
                    }
                }
            }
            else if(recommendedMoves.Length == 1)
            {
                moveToExecute = recommendedMoves[0];
            }


            if (moveCounter != 63 && recommendedMoves.Length != 0)
            {
                int previousX = X;//These are just to show where the knight was before it moved.
                int previousY = Y;//""
                X += moveX[moveToExecute];
                Y += moveY[moveToExecute];
                moveCounter++;

                Console.WriteLine("SuperKnight has moved from ({0}, {1}) to ({2}, {3})", previousX, previousY, X, Y);
                string quit = Console.ReadLine();
                if (quit == "quit")
                {
                    GameLoop.status = GameLoop.gameStatus.QUIT;
                }
            }
            else if (moveCounter == 63)
            {
                Console.WriteLine("YOU WIN! CONGRATULATIONS YOU DID ABSOLUTELY NOTHING!");
                Console.WriteLine("Thanks for playing!\nGoodbye!");
                Console.ReadLine();
                GameLoop.status = GameLoop.gameStatus.QUIT;
            }
            else
            {
                Console.WriteLine("There are no possible moves left -_____-");
                Console.ReadLine();
                Console.ReadLine(); 
                GameLoop.status = GameLoop.gameStatus.QUIT;
            }

            AdjustAccessibility();
            
        }

        private void AdjustAccessibility()
        {
            for (int row = 0; row < ChessBoard.chessBoard.GetLength(0); row++)
            {
                for (int column = 0; column < ChessBoard.chessBoard.GetLength(1); column++)
                {
                    accessibility[row, column] = GetNumberOfAvailableMoves(row, column);
                }
            }
        }

        private bool CheckMove(int move, int tempX, int tempY)
        {
            if (tempX + moveX[move] < 0 || tempX + moveX[move] > 7 || tempY + moveY[move] < 0 || tempY + moveY[move] > 7)
            {
                return false;
            }
            else if (ChessBoard.chessBoard[tempX + moveX[move], tempY + moveY[move]] == 1)
            {
                return false;
            }
            else
                return true;
        }

        private int GetNumberOfAvailableMoves(int positionX, int positionY)
        {
            int numberOfAvailableMoves = 0;

            for (int i = 0; i < 8; i++)
            {
                if (CheckMove(i, positionX, positionY))
                {
                    numberOfAvailableMoves++;
                }
            }

            return numberOfAvailableMoves;
        }

        public void GetAvailableMoves(int temX,int temY)
        {
            availableMoves.Clear();

            bool[] possibleMoves = new bool[8];

            for (int i = 0; i < 8; i++)
            {
                possibleMoves[i] = CheckMove(i, temX, temY);

                if (possibleMoves[i])
                {
                    availableMoves.Add(i);
                }
            }

        }

        public List<int> GetRecommendedMoves(List<int> moves, int xValue, int yValue)
        {
            int[] movesAvailable = moves.ToArray();
            int bestMove = 8;

            for (int i = 0; i < movesAvailable.Length; i++)//Test to see which has the lowest accessibility value and set that value to the best move.
            {
                int tempX = xValue;
                int tempY = yValue;

                tempX += moveX[movesAvailable[i]];
                tempY += moveY[movesAvailable[i]];

                if (accessibility[tempX, tempY] < bestMove)
                {
                    bestMove = accessibility[tempX, tempY];
                }

            }

            List<int> recommendedMoves = new List<int>();

            for (int i = 0; i < movesAvailable.Length; i++)
            {
                int tempX = xValue;
                int tempY = yValue;
                
                tempX += moveX[movesAvailable[i]];
                tempY += moveY[movesAvailable[i]];

                if (accessibility[tempX, tempY] == bestMove)
                {
                    recommendedMoves.Add(movesAvailable[i]);
                }
                
            }

            return recommendedMoves;
        }
    }
}
