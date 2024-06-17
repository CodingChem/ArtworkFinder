using ArtworkFinder.Library.Models;

namespace ArtworkFinder.CLI.Pages;
public interface IPageHandler
{
	public void SetPage(IPage page);
	public void DisplayPage();
	public List<FpackDataModel> GetFinishedFpack();
	public List<FpackDataModel> GetUnfinishedFpack();
	public void AddFpack(FpackDataModel fpack);
}
