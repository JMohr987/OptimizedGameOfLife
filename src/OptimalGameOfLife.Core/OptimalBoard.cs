namespace OptimalGameOfLife.Core;

// Board that contains the cells
// Uses a dictionary to contain the cells
// Look up by coordinate point to make sending signals easy
// The board add cells that recieve a signal that do not exist in memory.
// Removes cells from memory that have zero neighbors as a cell that recieves
// no signals will forever be off. Makes a glider in a 10,0000 X 10,000 not allocate too much memory!

public class OptimalBoard
{
	// Still uses the two board method
	public Dictionary<Point, Cell> CurrentBoard { get; private set; }
	public Dictionary<Point, Cell> NextBoard { get; private set; }
	// Rows and Cols for looping stuff
	public int Rows { get; private set; }
	public int Cols { get; private set; }

	// Assigns dimensions and creates the starting cells
	public OptimalBoard(int r, int c, List<Point> onCells)
	{
		Rows = r;
		Cols = c;

		CurrentBoard = new Dictionary<Point, Cell>(new PointProcessor());
		NextBoard = new Dictionary<Point, Cell>(new PointProcessor());

		for (int i = 0; i < onCells.Count; i++)
		{
			if (!CurrentBoard.ContainsKey(onCells[i]))
			{
				CurrentBoard.Add(onCells[i], new Cell(State.Alive));
			}
		}
	}

	// Makes sure the cell being checked is in inside the grid
	public bool ValidateCoordinates(int row, int col)
	{
		if ((row < 0) || (row > (Rows - 1)))
		{
			return false;
		}
		else if ((col < 0) || (col > (Cols - 1)))
		{
			return false;
		}

		return true;
	}

	// Sends a signal to a cell's neighbors
	// If no neighbor, creates a dead cell to send the signal to
	public void ProcessSignal(int signal, int row, int col)
	{
		for (int i = -1; i <= 1; i++)
		{
			for (int k = -1; k <= 1; k++)
			{
				if (k == 0 && i == 0)
				{
					continue;
				}
				if (!ValidateCoordinates(row + i, col + k))
				{
					continue;
				}

				if (!NextBoard.ContainsKey(new Point(col + k, row + i)))
				{
					Point tempKey = new Point(col + k, row + i);
					NextBoard.Add(tempKey, new Cell());
					NextBoard[tempKey].HandleSignal(signal);
				}
				else
				{
					NextBoard[new Point(col + k, row + i)].HandleSignal(signal);
				}

			}

		}
	}

	// The second board works a bit differently
	// It creates a clone of the current board to keep the alive cells and their neighbor count
	// Any new cells are added to this next board
	// Signals come from the current board and are sent to the new board
	// Avoids interferance between updating cells, signals sent, and new cells
	public void CalculateBoard()
	{
		NextBoard = new Dictionary<Point, Cell>(CurrentBoard, new PointProcessor());

		foreach (KeyValuePair<Point, Cell> cell in CurrentBoard)
		{
			// No need to process a signal that does nothing
			if(cell.Value.Signal == 0)
			{
				continue;
			}
			ProcessSignal(cell.Value.Signal, cell.Key.Y, cell.Key.X);
		}

		// Clones all cells with a non-zero neighbor count to a temp dictionary
		// Removes all zero neighbor count cells.
		Dictionary<Point, Cell> temp = new Dictionary<Point, Cell>(new PointProcessor());
		foreach (KeyValuePair<Point, Cell> cell in NextBoard)
		{
			if (cell.Value.NeighborCount <= 0)
			{
				continue;
			}
			cell.Value.CalculateState();
			temp.Add(cell.Key, cell.Value);
		}

		//Updates board for next generation
		CurrentBoard = temp;
	}
}
