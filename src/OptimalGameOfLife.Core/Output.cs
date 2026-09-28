namespace OptimalGameOfLife.Core;
//Used microsoft documentation to help me learn how to write data to a file
//https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file
//
//Simply takes the data from the variables and puts it into an output file
public static class Output
{
	private const string dataDir = "data";
	private static string fileName;
	public static void WriteData(OptimalBoard board, int generations)
	{
		int height = board.Rows;
		int width = board.Cols;
		int liveCells = 0;

		// Name based off dimensions to avoid overriting
		fileName = "Output" + height.ToString() + ".txt";

		List<Point> cellCoords = new List<Point>();

		// Adds up the live cells and gets the coordinates of them
		foreach (KeyValuePair<Point, Cell> cell in board.CurrentBoard)
		{
			if(cell.Value.CellState == State.Alive)
			{
				liveCells += 1;
				cellCoords.Add(new Point(cell.Key.X, cell.Key.Y));
			}
		}

		// Converts vars to strings
		string dimensions = width.ToString() + "," + height.ToString();
		string gens = generations.ToString();
		string live = liveCells.ToString();

		// Combines all of the variables into one big string
		string resultString = dimensions + "|" + gens + "|" + live + "|";

		for(int i = 0; i < cellCoords.Count; i++)
		{
			resultString += (cellCoords[i].X).ToString() + "," + (cellCoords[i].Y).ToString();

			if (i < (cellCoords.Count - 1))
			{
				resultString += "|";
			}
		}

		// Separates string into different strings to format for the stream writer
		string[] lines = resultString.Split('|');

		//Stream writer writes each string in the lines arrary to it's own line in the output file
		//Using block so the writer cloeses itself once done
		using (StreamWriter outputFile = new StreamWriter(Path.Combine(dataDir, fileName)))
		{
			foreach(string line in lines) 
			{
				outputFile.WriteLine(line);
			}
		}
	}
}
