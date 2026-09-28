namespace OptimalGameOfLife.Core;

// Class that contains the cell's data
// It's easier to store its data in individual cells than to
// make the game board do everything
//
// The cells work by sending signals to its neighbors on a state change
// if a cell turns on, it sends a +1 signal to the neighbors
// If it turns off, it sends a -1 signal to the neighbors
// If state does not change, the signal is set to zero and is not processed
// This method accurately keeps track of neighbor counts without proccessing every cell
// of the board and without counting neighbor counts for each cell when nothing has changed
// Also makes adding and removing cells from memory really easy
public class Cell
{
	// Three variables needed for calculations
	public State CellState { get; private set; }
	public int NeighborCount { get; private set; }
	public int Signal { get; private set; }

	// Default constructor to make a dead cell
	public Cell()
	{
		CellState = State.Dead;
		Signal = 0;
		NeighborCount = 0;
	}

	// Constructor used to set cells as on when the simulation starts
	public Cell(State s)
	{
		CellState = s;
		if (s == State.Alive)
		{
			Signal = 1;
		}
		else
		{
			Signal = 0;
		}

		NeighborCount = 0;
	}

	// Adds signal to neighbor count
	public void HandleSignal(int signal)
	{
		NeighborCount += signal;
	}

	// This switch statement is the rules broken down to the minimum amount of cases
	// Since a cell with zero, one, and four cells will always be dead in the next
	// generation, all three of those situations are combined in the default statement
	// For two neighbors the state remains the same whether dead or alive
	// And three neighbors is always alive.
	// Each case that results in a state change checks if it occurs and sets the appropriate signal.
	public void CalculateState()
	{
		switch (NeighborCount)
		{
			case 2:
				Signal = 0;
				break;
			case 3:
				if (CellState != State.Alive)
				{
					Signal = 1;
				}
				else
				{
					Signal = 0;
				}
				CellState = State.Alive;
				break;
			default:
				if (CellState != State.Dead)
				{
					Signal = -1;
				}
				else
				{
					Signal = 0;
				}
				CellState = State.Dead;
				break;

		}
	}
}
