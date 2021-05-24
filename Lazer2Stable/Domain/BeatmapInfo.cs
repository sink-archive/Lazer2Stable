namespace Lazer2Stable.Domain
{
	public class BeatmapInfo
	{
		public virtual int            ID             { get; set; }
		public virtual BeatmapSetInfo BeatmapSetInfo { get; set; }
		public virtual string         Hash           { get; set; }
		public virtual string         Path           { get; set; }
	}
}