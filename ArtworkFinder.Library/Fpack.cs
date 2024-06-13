using ArtworkFinder.Library.Events;
using ArtworkFinder.Library.Models;
namespace ArtworkFinder.Library;

internal class Fpack
{
    internal event EventHandler<SearchCompletedEventArgs>? SearchCompletedEvent;
    public string CustomerName { get; private set; }
    public string ItemNumber { get; private set; }
    public bool IsSearching { get; private set; }
    public List<ArtworkDataModel> ArtworkFiles { get; private set; }

    public Fpack(string customerName, string itemNumber)
    {
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        ItemNumber = itemNumber ?? throw new ArgumentNullException(nameof(itemNumber));
        IsSearching = false;
        ArtworkFiles = new();
    }
    internal void RunSearch(string[] customerDirectories)
    {
        IsSearching = true;
        var customerDirectory = customerDirectories.FirstOrDefault(x => x.Contains(CustomerName, StringComparison.OrdinalIgnoreCase));
        if (customerDirectory == null)
        {
            IsSearching = false;
            SearchCompletedEvent?.Invoke(this, new SearchCompletedEventArgs(false, ToString()));
            return;
        }
        var itemDirectories = Directory.GetDirectories(customerDirectory, "Item");
        if (itemDirectories.Length == 0)
        {
            foreach (var dir in Directory.GetDirectories(customerDirectory))
            {
                var itemDirectory = Directory.GetDirectories(dir, "Item");
                if (itemDirectory.Length != 0)
                    itemDirectories = itemDirectories.Concat(itemDirectory).ToArray();
            }
        }
        foreach (var item_dir in itemDirectories)
        {
            var paths = Directory.GetFiles(item_dir, "*" + ItemNumber + "*", SearchOption.AllDirectories);
            foreach (var path in paths)
            {
                ArtworkFiles.Add(
                    new ArtworkDataModel(path)
                );
            }
        }
        IsSearching = false;
        SearchCompletedEvent?.Invoke(this, new SearchCompletedEventArgs(ArtworkFiles.Count > 0, ToString()));
    }
    public override string ToString()
    {
        return $"{CustomerName}: {ItemNumber}";
    }
    public FpackDataModel ToFpackDataModel()
    {
        return new FpackDataModel(CustomerName, ItemNumber, IsSearching ? null : ArtworkFiles);
    }
}
