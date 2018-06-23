using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TicTacToe
{
    class Program
    {
        private static string[,] board =
            {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };
        private static int currentPlayer = 0;

        private static bool gameState = false;

        static void Main(string[] args)
        {
            PlayGame();
        }

        public static void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("     |     |");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[0,0], board[0, 1], board[0, 2]);
            Console.WriteLine("     |     |");
            Console.WriteLine("-----------------");
            Console.WriteLine("     |     |");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[1, 0], board[1, 1], board[1, 2]);
            Console.WriteLine("     |     |");
            Console.WriteLine("-----------------");
            Console.WriteLine("     |     |");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[2, 0], board[2, 1], board[2, 2]);
            Console.WriteLine("     |     |\n");
        }

        public static void PlayGame()
        {
            while (!gameState)
            {
                PrintBoard();
                UserMove();
                CheckForWin();
                if (!gameState) { currentPlayer = currentPlayer == 0 ? 1 : 0; }
            }

            PrintBoard();

            if (!CheckForDraw()) { Console.WriteLine("Congradulations player {0}. type \"n\" to quit or just press return to play again.", currentPlayer + 1); }
            else { Console.WriteLine("Draw! type \"n\" to quit or just press return to play again."); }
            string playAgain = Console.ReadLine();

            playAgain = playAgain.ToLower();

            if (playAgain != "n")
            {
                ResetGame();

                PlayGame();
            }
        }

        public static void UserMove()
        {
            Console.Write("Player {0}'s Turn. Choose a spot: ", currentPlayer + 1);
            GetUserInput();
        }

        private static void GetUserInput()
        {
            string place = Console.ReadLine();
            int placeInt = CheckRange(place);

            int[] boardPlace = GetPlace(placeInt);

            Debug.Write(board[boardPlace[0], boardPlace[1]]);

            if (board[boardPlace[0], boardPlace[1]] != "X" && board[boardPlace[0], boardPlace[1]] != "O") { board[boardPlace[0], boardPlace[1]] = currentPlayer == 0 ? "X" : "O"; }
            else
            {
                Console.Clear();
                PrintBoard();
                Console.WriteLine("Piece already taken, pick again! Player {0}'s Turn. Choose a spot: ", currentPlayer + 1);
                GetUserInput();
            }
        }

        private static int CheckRange(string piece)
        {
            int place = CheckValidNumber(piece);

            while (place > 9 || place < 1)
            {
                Console.Clear();
                PrintBoard();
                Console.WriteLine("You entered a incorrect place number, try Again! Player {0}'s Turn. Choose a spot: ", currentPlayer + 1);
                piece = Console.ReadLine();
                place = CheckValidNumber(piece);
            }

            return place;
        }

        public static int CheckValidNumber(string piece)
        {
            int pieceInt;

            while (!int.TryParse(piece, out pieceInt))
            {
                Console.Clear();
                PrintBoard();
                Console.WriteLine("You did not enter a valid number, try Again! Player {0}'s Turn. Choose a spot: ", currentPlayer + 1);
                piece = Console.ReadLine();
            }

            return pieceInt;
        }

        private static int[] GetPlace(int place)
        {
            switch(place)
            {
                case 1: return new int[] { 0, 0 };
                case 2: return new int[] { 0, 1 };
                case 3: return new int[] { 0, 2 };
                case 4: return new int[] { 1, 0 };
                case 5: return new int[] { 1, 1 };
                case 6: return new int[] { 1, 2 };
                case 7: return new int[] { 2, 0 };
                case 8: return new int[] { 2, 1 };
                case 9: return new int[] { 2, 2 };
                default: return new int[] { -1, -1 };
            }
        }

        private static void CheckForWin()
        {
            string piece = currentPlayer == 0 ? "X" : "O";

            Debug.WriteLine(" {0}", piece);

            if (board[0, 0] == piece && board[0, 1] == piece && board[0, 2] == piece ||
                board[1, 0] == piece && board[1, 1] == piece && board[1, 2] == piece ||
                board[2, 0] == piece && board[2, 1] == piece && board[2, 2] == piece ||
                board[0, 0] == piece && board[1, 0] == piece && board[2, 0] == piece ||
                board[0, 1] == piece && board[1, 1] == piece && board[2, 1] == piece ||
                board[0, 2] == piece && board[1, 2] == piece && board[2, 2] == piece ||
                board[0, 0] == piece && board[1, 1] == piece && board[2, 2] == piece ||
                board[0, 2] == piece && board[1, 1] == piece && board[2, 0] == piece)
            {
                Debug.WriteLine("Winner declared");
                gameState = true;
            }
            else if (CheckForDraw())
            {
                gameState = true;
            }
            else
            {
                Debug.WriteLine("Keep playing!");
                gameState = false;
            }
        }

        private static bool CheckForDraw()
        {
            int count = 0;

            foreach (string spot in board)
            {
                if (spot == "X" || spot == "O") count++;
            }

            return count == 9;
        }

        private static void ResetGame()
        {
            string[,] boardTemp =
            {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };

            board = boardTemp;

            currentPlayer = 0;

            gameState = false;
        }
    }
}
;