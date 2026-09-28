namespace OptimalGameOfLife.Core;

// A simple class that contains a bool and a string
// Used for errors so a function can return both a result and a debug message
public class Message
{
	public bool success;
	public string message;

	public Message(bool s, string m)
	{
		success = s;
		message = m;
	}
}
