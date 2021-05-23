namespace Lazer2Stable.Domain
{
	public class BeatmapInfo
	{
		public int    ID               { get; set; }
		public int    BeatmapSetInfoID { get; set; }
		public string Hash             { get; set; }
		public string Path             { get; set; }
	}
}