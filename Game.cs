using System;
using System.Collections.Generic;

namespace MineSweeper
{

    public class Game
    {
        public Board b; //creating an object of class Board

        char[,] board; //2D array to store board

        List<char[,]> boards = new List<char[,]>();

        public void Start()
        {
            int r = 0;
            int c = 0;
            int m = 0;
            int count = 0;

            
            do
            {
                //user inputing number of rows, columns and mines found in the board
                Console.Write("Rows: ");
                r = Convert.ToInt32(Console.ReadLine());
                Console.Write("Columns: ");
                c = Convert.ToInt32(Console.ReadLine());

                if (r != 0 && c != 0)
                {
                    Console.Write("Max number of Mines: ");
                    m = Convert.ToInt32(Console.ReadLine());
                }

                //if rows and columns are not equal to 0, board is set up based on user input
                if (r != 0 && c != 0) { 
                    b = new Board(r, c);
                    b.SetUpBoard(m);
                    board = b.GetBoard();
                    b.PrintBoard();

                    //array is traversed to count the number of neighbouring mines for each element
                    for (int i = 0; i < r; i++)
                    {
                        for (int j = 0; j < c; j++)
                        {
                            if (board[i, j] != '*') { 
                                int mineCount = CountNeighbourMines(i, j);
                                board[i, j] = (char)(mineCount + 48);
                            }  
                        }
                    }

                    boards.Add(board); //filling up list with boards that contain information about number of neighbouring mines
                }

                Console.WriteLine();
            } while (r != 0 && c != 0);

            //commencing printing of results - each array in the list is sent to be printed
            for(count=0; count<boards.Count; count++)
            {
                Console.WriteLine("Field #" + (count + 1));
                PrintMineCountBoard(boards[count]);
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        //counting the number of mines found in the surrounding 8 cells
        public int CountNeighbourMines(int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    //making sure we're not referencing cells out of bounds of the array
                    if (i>=0 && j>=0 && i<board.GetLength(0) && j<board.GetLength(1))
                    {
                        if (board[i, j] == '*')
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        //printing the boards that contain information about number of neighbouring mines
        public void PrintMineCountBoard(char [,] neighboursBoard)
        {
            
            for (int i = 0; i < neighboursBoard.GetLength(0) ; i++)
            {
                for (int j = 0; j < neighboursBoard.GetLength(1); j++)
                {
                    Console.Write(neighboursBoard[i, j]);
                    if (j == neighboursBoard.GetLength(1) - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            
        } 
    } 
}


