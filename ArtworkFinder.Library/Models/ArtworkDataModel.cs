namespace ArtworkFinder.Library.Models;
public class ArtworkDataModel
{
	public ArtworkDataModel(string path)
	{
		Path = path;
	}

	public string Path { get; set; }
    public string Name { get { return Path.Split('/')[Path.Split('/').Length - 1]; } }
    public bool IsArchived { get { return Path.Contains("rchi"); } }
    public DateTime LastModified { get { return File.GetLastWriteTime(Path); } }
}
