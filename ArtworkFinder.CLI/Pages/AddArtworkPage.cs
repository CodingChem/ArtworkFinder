using ArtworkFinder.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkFinder.CLI.Pages
{
	internal class AddArtworkPage : AbstractPage
	{
		public AddArtworkPage(IPageHandler pageHandler) : base(pageHandler)
		{

		}

		public override void Display()
		{
			Console.Clear();
			Console.WriteLine("--- Artwork Finder ---");
			Console.WriteLine();
			Console.Write("Add Artwork: ");
			string? userInput = Console.ReadLine();
			HandleInput(userInput);
			
		}

		private void HandleInput(string? userInput)
		{
			if (userInput == null) Display();
			if (userInput == "q") MainMenuPage();
			string[] userInputArray = userInput.Split(" ");
			if (userInputArray.Length != 2) Display();
			PageHandler.AddFpack(new FpackDataModel(userInputArray[0], userInputArray[1]));
			Display();
		}

		private void MainMenuPage()
		{
			PageHandler.SetPage(new MainMenuPage(PageHandler));
		}
	}
}
