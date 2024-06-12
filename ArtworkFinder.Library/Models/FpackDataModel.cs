namespace ArtworkFinder.Library.Models;

public class FpackDataModel
{
	public FpackDataModel(string customerName, string itemNumber)
	{
		CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
		ItemNumber = itemNumber ?? throw new ArgumentNullException(nameof(itemNumber));
		ArtworkFiles = new();
	}

	public string CustomerName { get; set; }
        public string ItemNumber { get; set; }
        public List<ArtworkDataModel> ArtworkFiles { get; set; }

}
