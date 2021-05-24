namespace Lazer2Stable.Domain
{
	public class BeatmapSetFileInfo
	{
		public virtual int            ID             { get; set; }
		public virtual BeatmapSetInfo BeatmapSetInfo { get; set; }
		public virtual FileInfo       FileInfo       { get; set; }
		public virtual string         Filename       { get; set; }
	}
}