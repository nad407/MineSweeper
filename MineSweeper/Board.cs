using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    public class Board
    {
        private int row;
        private int column;
        private int mines;
        private int mineCount = 0;
        private char[,] board; //multidimensional array to store minesweeper grid
        private char[] options = new char[] { '*', '.'}; //board pieces
        private int[,] randCoOrds; //multidimensional array to store a random, unique list of grid co-ordinates
        private Random rnd = new Random();


        //constructor
        public Board(int row, int column)
        {
            this.row = row;
            this.column = column;

            board = new char[row, column];
        }

        //return number of rows in a board
        public int GetRow()
        {
            return row;
        }

        //return number of columna in a board
        public int GetCol()
        {
            return column;
        }

        //the user will manually fill up the board with safe spaces and mines
        public bool SetUpBoardManually()
        {
            Console.WriteLine("\n");
            string newline;

            Console.Write("   ");
            for (int j=1; j<=column; j++)
            {
                Console.Write(j);
            }

            Console.WriteLine();

            for (int i=0; i<row; i++)
            {
                Console.Write(i + 1 + ": ");
                newline = Console.ReadLine();
                var pieces = newline.ToCharArray();
                bool valid = Validate(pieces); //check if board pieces entered by the user are valid
                if (valid == true)
                {
                    for (int j = 0; j < column; j++)
                    {
                        board[i, j] = Convert.ToChar(pieces[j]);
                    }
                }

                if(valid == false)
                {
                    Console.WriteLine("Invalid User Input. Accepted pieces = '*' and '.' Try Again.");
                    return false;
                }
            }

            return true;
            
        }

        //randomly filling up board with safe spaces and mines
        public void SetUpBoardAutomatically(int mines)
        {
            this.mines = mines;

            randCoOrds = new int[row*column, 2]; //defining size of multidimensional array to store all co-ordinates of minesweeper grid

           for (int y = 0; y < (row*column); y++)
           {
                var values = NewRandomCoOrds(row, column, randCoOrds, y);
                randCoOrds[y, 0] = values.Item1;
                randCoOrds[y, 1] = values.Item2;
           }

            //the whole grid is traversed randomly based on the coordinates stored in randCoOrds[,]
            for (int i=0; i<row*column; i++)
            {
                int coOrd1 = randCoOrds[i,0];
                int coOrd2 = randCoOrds[i,1];
                char option = options[rnd.Next(options.Length)];
                if (option == '*')
                {
                    mineCount++;
                    if (mineCount > mines)
                    {
                        option = '.';
                    }
                }
                board[coOrd1, coOrd2] = option;   
            }
            
        }

        //return board completed with all pieces
        public char [,] GetBoard()
        {
            return board;
        }

        //printing board with safe spaces and mines 
        public void PrintBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write(board[i, j]);
                    if (j == column - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        //generating a pair of random and unique coordinates
        private (int,int) NewRandomCoOrds(int r, int c, int[,]rc, int counter)
        {
            int coOrd1 = rnd.Next(0, r);
            int coOrd2 = rnd.Next(0, c);

            for (int i=0; i<counter; i++)
            {
                if ((rc[i,0] == coOrd1) & (rc[i,1] == coOrd2))
                {
                    return NewRandomCoOrds(r, c, rc, counter);
                }
            }
            return (coOrd1, coOrd2);
        }

        //validate the pieces entered manually in the grid by the user
        private bool Validate(char [] p)
        {
            for(int i=0; i<p.Length; i++)
            {
                //pieces entered by the user should only be * or .
                if((p[i].CompareTo('.')!=0) & (p[i].CompareTo('*')!=0))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
