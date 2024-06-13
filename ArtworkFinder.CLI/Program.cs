using ArtworkFinder.Library;

namespace ArtworkFinder.CLI;

internal class Program
{
	private static FpackList fpackList { get; set; } = new FpackList();
	private static bool exitFlag = false;
	private static string DisplayMessage { get; set; } = "";
        static void Main(string[] args)
	{
		while (!exitFlag) 
		{
			DisplayMainMenu(DisplayMessage);
		}
	}
	private static void DisplayMainMenu(string message = "")
	{
		Console.Clear();
		Console.WriteLine("--- ArtworkFinder ---");
		Console.WriteLine();
		Console.WriteLine(message);
		Console.WriteLine();
		Console.WriteLine($"Queued Fpacks: {fpackList.GetQueuedTasks()}");
		Console.WriteLine($"Found  Fpacks: {fpackList.GetNumberOfFpacks()}");
		Console.WriteLine("Select action:");
		Console.WriteLine("1) Add Fpack to Que");
		Console.WriteLine("2) View Artwork");
		Console.WriteLine("q) Quit\n");
		Console.Write("Action: ");
		string? userInput = Console.ReadLine();
		if (userInput == null | userInput == "")
		{
			DisplayMessage = "No option selected";
			return;
		}
		switch (userInput)
		{
			case "q":
				exitFlag = true;
				return;
			case "1":
				DisplayAddFpackMenu();
				return;
			case "2":
				DisplayViewFpackMenu();
				return;
			default:
				DisplayMessage = $"Error: {userInput} is not a valid option.";
				return;
		}
	}
	private static void DisplayAddFpackMenu()
	{
		Console.Clear();
		while (true)
		{
			Console.Write("Enter Customer and Batchnumber: ");
			string? userInput = Console.ReadLine();
            if ( userInput == null | userInput == "")
            {
				return;
            }
			string[] userInputArray = userInput.Split(' ');
			if (userInputArray.Length != 2) continue;
            string customerName = userInputArray[0];
			string itemNumber = userInputArray[1];
			fpackList.AddFpack(customerName, itemNumber);
		}
	}
	private static void DisplayViewFpackMenu()
	{
		throw new NotImplementedException();
	}
}
