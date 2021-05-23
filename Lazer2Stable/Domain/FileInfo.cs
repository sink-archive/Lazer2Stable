namespace Lazer2Stable.Domain
{
	public class FileInfo
	{
		public int    ID             { get; set; }
		public string Hash           { get; set; }
		public int    ReferenceCount { get; set; }
	}
}