using ArtworkFinder.Library.Controllers;
using ArtworkFinder.Library.Events;
using ArtworkFinder.Library.Models;

namespace ArtworkFinder.Library;

public class ArtworkFinder
{
	public ArtworkFinder(string baseSearchPath)
	{
		CustomerDirectories = Directory.GetDirectories(baseSearchPath);
		FpackList = new List<Fpack>();
		WorkQue = new WorkQue();
	}

	public event EventHandler<string>? SearchProgressUpdatedEvent;
	private string[] CustomerDirectories { get; set; }
	private List<Fpack> FpackList {  get; set; }
	private WorkQue WorkQue { get; set; }
	public void AddFpack(FpackDataModel fpackData)
	{
		//Add Fpack to list
		Fpack fpack = new Fpack(fpackData.CustomerName, fpackData.ItemNumber);
		FpackList.Add(fpack);
		//Listen to event
		fpack.SearchCompletedEvent += Fpack_SearchCompletedEvent;
		//Add to work que
		WorkQue.EnqueueTask(() => fpack.RunSearch(CustomerDirectories));
		//Invoke searchprogress event.
		SearchProgressUpdatedEvent?.Invoke(this, $"{fpackData.CustomerName}: {fpackData.ItemNumber} -> is Searching for artwork.");
	}

	private void Fpack_SearchCompletedEvent(object? sender, SearchCompletedEventArgs e)
	{
		string message = e.ArtworkFound ? $"{e.Fpack} - Sucsessfully completed" : $"{e.Fpack} - No Artwork found";
		SearchProgressUpdatedEvent?.Invoke(this, message);
	}

	public List<FpackDataModel> GetAvailableFpack()
	{
		return FpackList.Where((x) => x.IsSearching == false).Select(x => x.ToFpackDataModel()).ToList();
	}
	public List<FpackDataModel> GetUnavailableFpack()
	{
		return FpackList.Where((x) => x.IsSearching).Select(x => x.ToFpackDataModel()).ToList();
	}
	public int GetSearchProgress()
	{
		int completed = FpackList.Where(x => x.IsSearching != false).Count();
		return (int) (completed *100/ FpackList.Count() );
	}
}
