namespace Lazer2Stable.DbClasses
{
	public class BeatmapSetInfo
	{
		public virtual int             ID                 { get; set; }
		public virtual int?            OnlineBeatmapSetID { get; set; }
		public virtual int             MetadataId         { get; set; }
	}
}