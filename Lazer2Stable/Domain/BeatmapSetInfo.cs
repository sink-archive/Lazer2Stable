namespace Lazer2Stable.Domain
{
	public class BeatmapSetInfo
	{
		public virtual int             ID                 { get; set; }
		public virtual int?            OnlineBeatmapSetID { get; set; }
		public virtual BeatmapMetadata Metadata           { get; set; }
	}
}