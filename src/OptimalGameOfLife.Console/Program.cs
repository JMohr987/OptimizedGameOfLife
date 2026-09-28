using OptimalGameOfLife.Core;

// Processes args and returns early if there is an error
ArgsProcessor argData = new ArgsProcessor();
Message result = argData.HandleArgs(args);
if (!result.success)
{
	Console.WriteLine(result.message);
	return;
}
// createsthe board
OptimalBoard board = new OptimalBoard(argData.Rows, argData.Cols, argData.Cells);

// Prints graphics
// "." for off
// "#" for on
if (argData.Graphics == true)
{
	for (int i = 0; i < argData.Generations; i++)
	{
		Console.WriteLine(i);
		for (int j = 0; j < board.Rows; j++)
		{
			for (int k = 0; k < board.Cols; k++)
			{
				if(!board.CurrentBoard.ContainsKey(new Point(k, j)))
				{
					Console.Write(".");
				}
				else if (board.CurrentBoard[new Point(k, j)].CellState == State.Alive)
				{
					Console.Write("#");
				}
				else
				{
					Console.Write(".");
				}
			}
		
			Console.Write("\n");
		}
		
		board.CalculateBoard();
		Thread.Sleep(250);
		Console.Clear();
	}
}
else 
// No graphics option
{
	for (int i = 0; i < argData.Generations; i++)
	{
		board.CalculateBoard();
	}
}
//Writes to file
Output.WriteData(board, argData.Generations);

