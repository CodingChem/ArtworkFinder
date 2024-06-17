using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkFinder.CLI.Pages
{
	internal class MainMenuPage : AbstractPage
	{
		public MainMenuPage(IPageHandler pageHandler) : base(pageHandler)
		{
		}

		public override void Display()
		{
			Console.Clear();
			Console.WriteLine("--- Artwork Finder ---");
			Console.WriteLine("");
			int foundFpack = PageHandler.GetFinishedFpack().Count();
			int searchingFpack = PageHandler.GetUnfinishedFpack().Count();
			Console.WriteLine($"{foundFpack} Fpack done searching. \n{searchingFpack} Fpack still searching.");
			Console.WriteLine();
			Console.WriteLine("a) Add new Fpack to que.");
			Console.WriteLine("b) View list of fpack");
			Console.WriteLine("q) Quit");
			Console.Write("\nSelect option: ");
			string? userInput = Console.ReadLine();
			HandleInput(userInput);
		}

		private void HandleInput(string? userInput)
		{
			switch (userInput)
			{
				case "q":
					return;
				case "a":
					NavigateAddArtworkPage();
					return;
				case "b":
					NavigateArtworkPage();
					return;
				default:
					Display();
					return;

			}
		}

		private void NavigateAddArtworkPage()
		{
			PageHandler.SetPage(new AddArtworkPage(PageHandler));
		}

		private void NavigateArtworkPage()
		{
			PageHandler.SetPage(new ArtworkPage(PageHandler));
		}
	}
}
