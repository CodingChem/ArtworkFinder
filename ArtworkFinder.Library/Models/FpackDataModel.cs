namespace ArtworkFinder.Library.Models;

public class FpackDataModel
{
	public string CustomerName { get; private set; }
	public string ItemNumber { get; private set; }
	public List<ArtworkDataModel>? ArtworkFiles { get; private set; }
    public FpackDataModel(string customerName, string itemNumber, List<ArtworkDataModel>? artworkFiles = null)
    {
        CustomerName = customerName;
        ItemNumber = itemNumber;
        ArtworkFiles = artworkFiles;
    }
	public override string ToString()
	{
		return $"{CustomerName} - {ItemNumber}";
	}
}
