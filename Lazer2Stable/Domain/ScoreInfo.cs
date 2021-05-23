namespace Lazer2Stable.Domain
{
	public class ScoreInfo
	{
		public virtual int         ID          { get; set; }
		public virtual BeatmapInfo BeatmapInfo { get; set; }
		public virtual string      Hash        { get; set; }
	}
}