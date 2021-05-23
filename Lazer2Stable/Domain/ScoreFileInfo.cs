namespace Lazer2Stable.Domain
{
	public class ScoreFileInfo
	{
		public int       ID        { get; set; }
		public FileInfo  FileInfo  { get; set; }
		public string    Filename  { get; set; }
		public ScoreInfo ScoreInfo { get; set; }
	}
}