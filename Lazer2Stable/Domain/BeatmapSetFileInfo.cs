namespace Lazer2Stable.Domain
{
	public class BeatmapSetFileInfo
	{
		public int      ID               { get; set; }
		public int      BeatmapSetInfoID { get; set; }
		public FileInfo FileInfo         { get; set; }
		public string   Filename         { get; set; }
	}
}