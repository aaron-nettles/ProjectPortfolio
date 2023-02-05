using System;
using System.Linq;
using System.Text;

class TicTacToe
{
    //create the board within the array
    static char[] board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int player = 1;
    static bool playComputer;
    static int player1Wins = 0;
    static int player2Wins = 0;
    static int computerWins = 0;
    static int ties = 0;

    static void Main(string[] args)
    {
        //main menu functions as well as title
        ShowTitle();
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
        ShowMenu();
        DrawBoard();

        Console.WriteLine("Do you want to play against the computer? (y/n)");
        string choice = Console.ReadLine();
        if (choice.ToLower() == "y")
        {
            playComputer = true;
        }
        else
        {
            playComputer = false;
        }
        //check to see if the cell is already occupied before allowing the player to make a move.
        while (true)
        {
            int move = int.Parse(Console.ReadLine());
            if (move == 10)
            {
                Environment.Exit(0);
            }
            else if (move == 11)
            {
                Restart();
                ShowMenu();
                DrawBoard();
            }
            else if (move >= 1 && move <= 9)
            {
                if (board[move - 1] != 'X' && board[move - 1] != 'O')
                {
                    if (player % 2 == 0)
                    {
                        board[move - 1] = 'O';
                    }
                    else
                    {
                        board[move - 1] = 'X';
                    }
                    player++;
                    DrawBoard();
                    //check who won and display
                    
                    if (CheckWin())
                    {
                        //the expression will return 1 if player is odd, or 2 if player is even.
                        if ((player % 2) + 1 == 1)
                        {
                            Console.WriteLine("Player 1 wins!\n");
                            player1Wins++;
                        }
                        else
                        {
                            Console.WriteLine("Player 2 wins!\n");
                            player2Wins++;
                        }
                        DisplayScore();
                        Console.ReadKey();
                        Restart();
                        ShowMenu();
                        DrawBoard();
                    }
                    else if (player == 10)
                    {
                        Console.WriteLine("It's a tie!\n");
                        ties++;
                        DisplayScore();
                        Console.ReadKey();
                        Restart();
                        ShowMenu();
                        DrawBoard();
                    }

                    //check for the computer win after computer move and added the message for computer win if it wins.
                    if (playComputer)
                    {
                        ComputerMove();
                        DrawBoard();
                        if (CheckWin())
                        {
                            Console.WriteLine("Computer wins!\n");
                            computerWins++;
                        }
                        else if (player == 10)
                        {
                            Console.WriteLine("It's a tie!\n");
                            ties++;
                        }
                        else if (CheckWin() && (player % 2) + 1 == 1)
                        {
                            Console.WriteLine("Player 1 wins!\n");
                            player1Wins++;
                        }
                        if (CheckWin() || player == 10)
                        {
                            DisplayScore();
                            Console.ReadKey();
                            Restart();
                            ShowMenu();
                            DrawBoard();
                        }
                    }

                }
                else
                //if a number already selected is chosen
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }
            else
            {
                //if a number further than 11 is selected
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
    /// <summary>
    ///
    ///
    /// methods below, starting with the menu
    ///
    ///
    ///
    ///
    /// </summary>
    static void ShowMenu()
    {
        Console.WriteLine("Welcome to Simple Tic Tac Toe!");
        Console.WriteLine("Player 1 is (X)  -  Player 2 is (O)");
        Console.WriteLine("Directions: To make a move, enter the number of the cell where you want to place your marker.");
        Console.WriteLine("Press 10 to Exit the game.");
        Console.WriteLine("Press 11 to restart the game");
    }
    //This method is called after each move to redraw the game board.
    //It uses the Console.Clear() method to clear the console, calls the ShowMenu() method to display the game menu,
    //and then uses a series of Console.WriteLine() statements to draw the Tic Tac Toe board using the characters stored in the board array.
    static void DrawBoard()
    {
        Console.Clear();
        ShowMenu();
        Console.WriteLine("     |     |     ");
        Console.WriteLine("  {0}  |  {1}  |  {2}  ", board[0], board[1], board[2]);
        Console.WriteLine("_____|_____|_____");
        Console.WriteLine("     |     |     ");
        Console.WriteLine("  {0}  |  {1}  |  {2}  ", board[3], board[4], board[5]);
        Console.WriteLine("_____|_____|_____");
        Console.WriteLine("     |     |     ");
        Console.WriteLine("  {0}  |  {1}  |  {2}  ", board[6], board[7], board[8]);
        Console.WriteLine("     |     |     ");

    }
    //This method checks if there are three of the same characters in a row, column,
    //or diagonal on the board, which would indicate that a player has won the game.
    static bool CheckWin()
    {
        // check rows
        if (board[0] == board[1] && board[1] == board[2])
            return true;
        if (board[3] == board[4] && board[4] == board[5])
            return true;
        if (board[6] == board[7] && board[7] == board[8])
            return true;

        // check columns
        if (board[0] == board[3] && board[3] == board[6])
            return true;
        if (board[1] == board[4] && board[4] == board[7])
            return true;
        if (board[2] == board[5] && board[5] == board[8])
            return true;

        // check diagonals
        if (board[0] == board[4] && board[4] == board[8])
            return true;
        if (board[2] == board[4] && board[4] == board[6])
            return true;

        return false;
    }
    //This method uses a random class to generate a random number between 0 and 8,
    //which corresponds to the index of a cell on the board.
    
    static void ComputerMove()
    {
        Random rnd = new Random();
        int move;
        //It uses a do-while loop that runs until a valid move is made.
        do
        {
            //Inside the do-while loop, I used the Random class to generate a random number between 0 and 8,
            //which corresponds to the index of a cell on the board. I check if the cell is empty, if it is then I set the cell to 'O' 
            move = rnd.Next(0, 9);
        } while (board[move] == 'X' || board[move] == 'O');
        board[move] = 'O';
        player++;
    }
    //records the number of wins on the board
    static void Restart()
    {
        board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        player = 1;
    }
    /// <summary>
    /// this is where you would display the score
    /// </summary>

    static void DisplayScore()
    {
        Console.WriteLine("Player 1 wins: {0}\nPlayer 2 wins: {1}\nComputer wins: {2}\nTies: {3}", player1Wins, player2Wins, computerWins, ties);
    }

    static void ShowTitle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("     _____ _____ _____");
        Console.WriteLine("    |     |     |     |");
        Console.WriteLine("    |  T  |  I  |  C  |");
        Console.WriteLine("    |_____|_____|_____|");
        Console.WriteLine("    |     |     |     |");
        Console.WriteLine("    |  T  |  A  |  C  |");
        Console.WriteLine("    |_____|_____|_____|");
        Console.WriteLine("    |     |     |     |");
        Console.WriteLine("    |  T  |  O  |  E  |");
        Console.WriteLine("    |_____|_____|_____|");
        Console.WriteLine("A Simple Game of Tic Tac Toe");
        Console.ResetColor();
    }

}




