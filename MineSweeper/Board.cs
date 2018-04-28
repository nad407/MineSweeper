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
        private char[,] board;
        private char[] options = new char[] { '*', '.', '.' }; //board pieces
        private int mineCount = 0;



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

        //setting up board with safe spaces and mines based on user input
        public void SetUpBoard(int mines) {

            this.mines = mines;

            //randomly filling up the board with safe spaces and mines
            Random rnd = new Random();
            for (int i = 0; i <row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    char option = options[rnd.Next(options.Length)];
                    if (option == '*')
                    {
                        mineCount++;
                        if (mineCount > mines)
                        {
                            option = '.';
                        }
                    }
                    board[i, j] = option;
                }
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
    }
}
