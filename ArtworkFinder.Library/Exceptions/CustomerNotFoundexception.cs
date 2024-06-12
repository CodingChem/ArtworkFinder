namespace ArtworkFinder.Library.Exceptions;

public class CustomerNotFoundException : Exception
{
    public string CustomerName { get; set; }

    public CustomerNotFoundException(string message, string customerName) : base (message)
	{
		CustomerName = customerName;
	}
}