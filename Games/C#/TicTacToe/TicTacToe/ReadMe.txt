
This code uses a simple array to represent the Tic Tac Toe board, where the numbers 1-9 correspond to the cells on the board.
The game menu is displayed at the beginning and explains how to play the game, and the game board is redrawn after each move.
The game checks for a win after each move and exits the game if there is a winner or if it's a tie.

To make the game playable against the computer, I added a variable called `playComputer` that is set to true or false based on the player's choice.
I also added a new method called `ComputerMove()` that simulates the computer's move by randomly selecting an available cell on the board. 
The `ComputerMove()` method is called after each player's move, if the player has chosen to play against the computer.
Inside the `ComputerMove()` method, I added a while loop that runs until a valid move is made.

Inside the while loop, I used the `Random` class to generate a random number between 0 and 8, which corresponds to the index of a cell on the board.
I check if the cell is empty, if it is then I set the cell to 'O' (since the computer is playing as 'O') and break out of the while loop.
If the cell is not empty, then I generate a new random number and repeat the process.
In the main loop, I also added a check to see if the cell is already occupied before allowing the player to make a move.

And I added an additional check for the computer win after computer move and added the message for computer win if it wins.
Please note that this is a basic version that can be improved by adding more advanced features such as a strategy for the computer to make more intelligent moves.
