namespace ArtworkFinder.Library.Models
{
    public class FpackDataModel
    {
        public string CustomerName { get; set; }
        public string ItemNumber { get; set; }
        public List<ArtworkDataModel> ArtworkFiles { get; set; }

    }
}
