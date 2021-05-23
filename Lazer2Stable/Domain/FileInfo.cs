namespace Lazer2Stable.Domain
{
	public class FileInfo
	{
		public virtual int    ID             { get; set; }
		public virtual string Hash           { get; set; }
		public virtual int    ReferenceCount { get; set; }
	}
}