using ArtworkFinder.Library.Models;
using System.Diagnostics;
using System.IO;

namespace ArtworkFinder.CLI.Pages
{
	internal class ArtworkPage : AbstractPage
	{
		List<FpackDataModel> SearchingFpack;
		List<FpackDataModel> FinishedFpack;
		public ArtworkPage(IPageHandler pageHandler) : base(pageHandler)
		{
			FinishedFpack = pageHandler.GetFinishedFpack();
			SearchingFpack = pageHandler.GetUnfinishedFpack();
		}

		public override void Display()
		{
			Console.Clear();
			Console.WriteLine("--- Artwork Finder ---");
			Console.WriteLine("");
			Console.WriteLine($"Searchprogress: {PageHandler.GetFinishedFpack().Count()} / {PageHandler.GetUnfinishedFpack().Count()}");
			Console.WriteLine("Finished Artwork:");
			int idx = 1;
			foreach (FpackDataModel fpack in PageHandler.GetFinishedFpack())
			{
				Console.WriteLine(idx + ") " + fpack.ToString());
				idx++;
			}
			Console.WriteLine("");
			Console.Write("Select Fpack: ");
			string userInput = Console.ReadLine();
			HandleInput(userInput);
			do
			{
				userInput = Console.ReadLine();
			}
			while (userInput == null);
			if (userInput == "q")
				SetPage(new MainMenuPage(PageHandler));
			string[] userInputArray = userInput.Split(' ');
		}

		private void HandleInput(string? userInput)
		{
			switch (userInput)
			{
				case null:
					Display();
					return;
				case "q":
					PageHandler.SetPage(new MainMenuPage(PageHandler));
					return;
				default:
					break;
			}
			bool success = int.TryParse(userInput, out int index);
			index--;
			if (!success) Display();
			FpackDataModel fpack;
			try
			{
				fpack = PageHandler.GetFinishedFpack()[index];
			}
			catch (IndexOutOfRangeException)
			{
				Console.WriteLine("Invalid Index");
				Console.Write("Press any key to continue.");
				return;
			}
			if (fpack.ArtworkFiles == null | fpack.ArtworkFiles.Count == 0) Display();
			foreach (ArtworkDataModel artwork in fpack.ArtworkFiles)
			{
				using Process fileopener = new Process();
				fileopener.StartInfo.FileName = "explorer";
				fileopener.StartInfo.Arguments = "\"" + artwork.Path + "\"";
				fileopener.Start();
			}
			Console.WriteLine("Openening artwork.");
			Console.Write("Press any key to continue.");
			Console.ReadLine();
			Display();
		}
	}
}