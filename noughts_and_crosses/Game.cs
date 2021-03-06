﻿using System;

namespace noughts_and_crosses
{
    public class Game
    {
        private Square[,] _board = new Square[3, 3];
        public void PlayGame()
        {
            Player player = Player.Crosses;

            bool @continue = true;
            while (@continue)
            {
                DisplayBoard();
                @continue = PlayMove(player);
                if (!@continue)
                    return;
                player = 3 - player; //swaps player between X and O
            }
        }

        private void DisplayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    Console.Write(" " + _board[i, j]);
                Console.WriteLine();
            }
        }

        private bool PlayMove(Player player)
        {
            Console.WriteLine("Invalid input quits game");
            Console.Write($"{player}: Enter row comma column, e.g. 3,3 > ");
            string input = Console.ReadLine();
            string[] parts = input.Split(',');
            if (parts.Length != 2)
                return false;
            int.TryParse(parts[0], out int row);
            int.TryParse(parts[1], out int column);

            if (row < 1 || row > 3 || column < 1 || column > 3)
                return false;

            if (_board[row - 1, column - 1].Owner != Player.Noone)
            {
                Console.WriteLine("Square is already occupied");
                return false;
            }

            var playerSquare = new Square(player);
            _board[row - 1, column - 1] = playerSquare;

            if (checkWinner(row - 1, column - 1, playerSquare.Owner))
            {
                DisplayBoard();
                Console.WriteLine($"The {player} player has won!");
                Console.WriteLine("");
                return false;
            }
            return true;
        }


        private bool checkWinner(int row, int column, Player currentPlayer)
        {
            int rowCounter = 0;
            int columnCounter = 0;

            for (int i = 0; i < 3; i++)
            {
                if (_board[row, i].Owner == currentPlayer)
                    rowCounter++;
                if (_board[i, column].Owner == currentPlayer)
                    columnCounter++;
            }

            if (rowCounter == 3 || columnCounter == 3)
                return true;

            if (row == 0 && column == 0 ||
                row == 2 && column == 2 ||
                row == 1 && column == 1 ||
                row == 2 && column == 0 ||
                row == 2 && column == 2)
            //if selection is a diagonal
            {
                int diagonalCounter1 = 0;
                int diagonalCounter2 = 0;

                int j = 2;
                for (int i = 0; i < 3; i++)
                {
                    if (_board[i, i].Owner == currentPlayer)
                        diagonalCounter1++;
                    if (_board[j, i].Owner == currentPlayer)
                        diagonalCounter2++;
                    j--;
                }
                if (diagonalCounter1 == 3 || diagonalCounter2 == 3)
                    return true;
            }

            return false;
        }

    }
}

