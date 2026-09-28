namespace OptimalGameOfLife.Core;

// Used Microsoft Documentation to help me write the Equality Comparer Class
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1?view=net-10.0
// This helper class is used so that my Point class can be used as a key for
// my dictionary. Without it, whenever I used "new Point()" it would result in false
// as it originally compares the refereances instead of the values.

public class PointProcessor : IEqualityComparer<Point>
{
	// This equals class is what the code will use for key equality comparisons
	// instead of the default reference comparison
	public bool Equals(Point p1, Point p2)
	{
		if (ReferenceEquals(p1, p2))
		{
			return true;
		}

		if(p2 is null || p1 is null)
		{
			return false;
		}

		return ((p1.X == p2.X) && (p1.Y == p2.Y));
	}
	// Creates the hashcode that the dictionary uses to quickly find the value
	public int GetHashCode(Point p)
	{
		return p.X ^ p.Y;
	}
}
