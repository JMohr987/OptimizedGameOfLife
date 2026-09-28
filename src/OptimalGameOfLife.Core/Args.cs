namespace OptimalGameOfLife.Core;

// Class that handles arguments and file input

public class ArgsProcessor
{
	// Has a variable for graphics and each part of the input file
	public bool Graphics { get; private set; }
	public int Rows { get; private set; } 
	public int Cols { get; private set; } 
	public int Generations { get; private set; } 
	public int LiveCells { get; private set; } 
	
	// This is the list of cells that start on
	public List<Point> Cells { get; private set; }
	
	// Uses a data dir and file name so that C# can use Path.Combine to make it OS independant
	private const string directory = "data";
	public string fileName;
	private string path;
	
	// This constructor assigns default values for everything
	// Though this combination of cells is not very fun!
	public ArgsProcessor()
	{
		Cells = new List<Point>();
		Cells.Add(new Point(0,0));
		Cells.Add(new Point(1,0));
		Cells.Add(new Point(0,1));
		Cells.Add(new Point(1,1));
		LiveCells = 4;
		Graphics = false;
		Rows = 10;
		Cols = 10;
		Generations = 10;
	}
	
	//This method gets and interprets the args 
	public Message HandleArgs(string[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i].Equals( "--graphics"))
			{
				Graphics = true;
			}

			else if ((args[i].Equals("--input")) && (i < (args.Length - 1)))
			{
				return ProcessFile(args[i+1]);

			}
		}

		return new Message(true, "Using default values, no file processed");

	}

	// Proccesses the file. First creates path, then checks if it is valid, then puts the data from it into the variables
	public Message ProcessFile(string file)
	{
		fileName = file;

		path = Path.Combine(directory, fileName);

		Message result = ValidateFile();
		if (result.success == false)
		{
			return result;
		}

		result = ParseFile();
		
		if (result.success == false)
		{
			return result;
		}

		return new Message(true, "Successfully processed file!");

	}

	// Checks if the dimensions of the file are correct and adds them to the code if correct
	public Message ValidDimensions(string dim)
	{
		string[] elements = dim.Split(',');
		int testInt;

		if (elements.Length != 2)
		{
			return new Message(false, "Invalid Dimensions");
		}

		if (!int.TryParse(elements[0].Trim(), out testInt))
		{
			return new Message(false, "Invalid Dimensions");
		}

		if (!int.TryParse(elements[1].Trim(), out testInt))
		{
			return new Message(false, "Invalid Dimensions");
		}

		if ((int.Parse(elements[0].Trim()) <= 0) || (int.Parse(elements[1].Trim()) <= 0))
		{
			return new Message(false, "Invalid Dimensions");
		}

		Cols = int.Parse(elements[0]);
		Rows = int.Parse(elements[1]);

		return new Message(true, "Success");
	}

	// Checks if the generation number in the file is correct and adds to code if correct
	public Message ValidGenerations(string dim)
	{
		string[] elements = dim.Split(',');
		int testInt;

		if (elements.Length != 1)
		{
			return new Message(false, "Invalid Generations");
		}

		if(!int.TryParse(elements[0].Trim(), out testInt))
		{
			return new Message(false, "Invalid Generations");
		}

		if(int.Parse(elements[0].Trim()) < 0)
		{
			return new Message(false, "Invalid Generations");
		}

		Generations = int.Parse(elements[0]);

		return new Message(true, "Success");
	}

	//Checks if the live cell count is correct and adds to code if correct
	public Message ValidLiveCells(string liv)
	{
		string[] elements = liv.Split(',');
		int testInt;

		if (elements.Length != 1)
		{
			return new Message(false, "Invalid Live Cell Count");
		}

		if(!int.TryParse(elements[0].Trim(), out testInt))
		{
			return new Message(false, "Invalid Live Cell Count");
		}

		if(int.Parse(elements[0].Trim()) < 0)
		{
			return new Message(false, "Invalid Live Cell Count");
		}
		LiveCells = int.Parse(elements[0]);

		return new Message(true, "Success");
	}

	//Checks if a cell's coordinates are valid and adds to code if correct
	public Message ValidCell(string dim)
	{
		string[] elements = dim.Split(',');
		int testInt;

		if (elements.Length != 2)
		{
			return new Message(false, "Invalid Coordinates");
		}

		if (!int.TryParse(elements[0].Trim(), out testInt))
		{
			return new Message(false, "Invalid Coordinates");
		}

		if (!int.TryParse(elements[1].Trim(), out testInt))
		{
			return new Message(false, "Invalid Coordinates");
		}

		if (((int.Parse(elements[0].Trim()) < 0) || (int.Parse(elements[0].Trim()) >= Cols)) || ((int.Parse(elements[1].Trim()) < 0) || int.Parse(elements[1].Trim()) >= Rows))
		{
			return new Message(false, "Invalid Coordinates");
		}

		Cells.Add(new Point(int.Parse(elements[0].Trim()), int.Parse(elements[1].Trim())));

		return new Message(true, "Success");
	}

	// Method that combines all the validation and adding methods. Returns the message of the
	// method that failed or returns a success message if succeeded.
	public Message ParseFile()
	{
        string[] lines = File.ReadAllLines(path);
		Message result;

		result = ValidDimensions(lines[0]);
		if (!result.success)
		{
			return result;
		}

		result = ValidGenerations(lines[1]);
		if (!result.success)
		{
			return result;
		}

		result = ValidLiveCells(lines[2]);
		if (!result.success)
		{
			return result;
		}

		string[] cellLines = lines[3..];
		Cells = new List<Point>();

		for(int i = 0; i < cellLines.Length; i++)
		{
			result = ValidCell(cellLines[i]);
			if (!result.success)
			{
				return result;
			}
		}
		
		return new Message(true, "Succesfully Read File");


	}

	//Checks if the file exists and is able to be opened
	public Message ValidateFile()
	{
		if(!File.Exists(path))
		{
			return new Message(false, "File does not exist! Put input file in data directory!");
		}

		try
		{
			FileStream fs = File.Open(path, FileMode.Open);
			fs.Close();
		}
		catch (Exception)
		{
			return new Message(false, "File could not be opened!");
		}

		return new Message(true, "File opened!");

	}
}
