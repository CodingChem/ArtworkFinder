using ArtworkFinder.CLI.Pages;

namespace ArtworkFinder.CLI;

public class Program
{
	public static void Main(string[] args)
	{
		if (args.Lenght != 2)
			throw InvalidArgumentException("Please provide the base search path")
		var run = new PageHandler(args[1]);
	}
}
