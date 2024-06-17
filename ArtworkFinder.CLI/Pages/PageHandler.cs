using ArtworkFinder.Library;
using ArtworkFinder.Library.Models;

namespace ArtworkFinder.CLI.Pages
{
	internal class PageHandler : IPageHandler
	{
		private IPage Page;
		private ArtworkSearcher ArtworkFinder;
		private List<FpackDataModel> FinishedFpack;
		private List<FpackDataModel> UnfinishedFpack;
        public PageHandler()
        {
			ArtworkFinder = new("V:\\01_Customers\\Active");
			FinishedFpack = ArtworkFinder.GetAvailableFpack();
			UnfinishedFpack = ArtworkFinder.GetUnavailableFpack();
			ArtworkFinder.SearchProgressUpdatedEvent += ArtworkFinder_SearchProgressUpdatedEvent;
			Page = new MainMenuPage(this);
			DisplayPage();
		}

		private void ArtworkFinder_SearchProgressUpdatedEvent(object? sender, string e)
		{
			FinishedFpack = ArtworkFinder.GetAvailableFpack();
			UnfinishedFpack = ArtworkFinder.GetUnavailableFpack();
		}

		public void DisplayPage()
		{
			Page.Display();
		}

		public List<FpackDataModel> GetFinishedFpack()
		{
			return ArtworkFinder.GetAvailableFpack();
		}

		public List<FpackDataModel> GetUnfinishedFpack()
		{
			return ArtworkFinder.GetUnavailableFpack();
		}

		public void SetPage(IPage page)
		{
			Page = page;
			DisplayPage();
		}
		public void AddFpack(FpackDataModel fpack)
		{
			ArtworkFinder.AddFpack(fpack);
		}
	}
}
