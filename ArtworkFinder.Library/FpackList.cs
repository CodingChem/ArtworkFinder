using ArtworkFinder.Library.Exceptions;
using ArtworkFinder.Library.Models;

namespace ArtworkFinder.Library;
public class FpackList
{
	private List<FpackDataModel> Fpacks { get; set; }
	private WorkQue WorkQue { get; set; }
	public string[] CustomerDirectories { get; set; }
	private int QueuedTasks { get; set; }
	public FpackList(string baseSearchPath = "V:\\01_Customers\\Active")
	{
		CustomerDirectories = GetCustomerDirectories(baseSearchPath) ?? throw new ArgumentNullException(nameof(baseSearchPath));
		WorkQue = new WorkQue();
		Fpacks = new List<FpackDataModel>();
		QueuedTasks = 0;
	}
	public void AddFpack(string customerName, string itemNumber)
	{
		var fpack = new FpackDataModel(customerName, itemNumber);
		WorkQue.EnqueueTask(() => AddArtworkToFpack(this, fpack, CustomerDirectories)); 
		QueuedTasks++;
	}
	public void AddFpack(FpackDataModel Fpack)
	{
		Fpacks.Add(Fpack);
	}
	public static void AddArtworkToFpack(FpackList self, FpackDataModel fpack, string[] customerDirectories)
	{
		var customerDirectory = customerDirectories.FirstOrDefault(x => x.Contains(fpack.CustomerName, StringComparison.OrdinalIgnoreCase));
		if (customerDirectory == null) { throw new CustomerNotFoundException("Not Found" ,fpack.CustomerName); }
		var itemDirectories = Directory.GetDirectories(customerDirectory, "Item");
		if (itemDirectories.Length == 0)
		{
			foreach (var dir in Directory.GetDirectories(customerDirectory))
			{
				var itemDirectory = Directory.GetDirectories(dir, "Item");
				if (itemDirectory.Length != 0)
					itemDirectories = itemDirectories.Concat(itemDirectory).ToArray<string>();
			}
		}
		foreach (var item_dir in itemDirectories)
		{
			var paths = Directory.GetFiles(item_dir, "*" + fpack.ItemNumber + "*", SearchOption.AllDirectories);
			foreach (var path in paths)
			{
				fpack.ArtworkFiles.Add(
					new ArtworkDataModel(path)
				);
			}
		}
		self.AddFpack(fpack);
		self.QueuedTasks--;
	}
	private string[] GetCustomerDirectories(string baseSearchPath)
	{
		return Directory.GetDirectories(baseSearchPath);
	}
	public int GetQueuedTasks()
	{
		return QueuedTasks;
	}
}
