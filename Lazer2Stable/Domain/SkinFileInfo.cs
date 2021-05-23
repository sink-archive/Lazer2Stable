namespace Lazer2Stable.Domain
{
	public class SkinFileInfo
	{
		public int      ID       { get; set; }
		public FileInfo FileInfo { get; set; }
		public string   Filename { get; set; }
		public SkinInfo SkinInfo { get; set; }
	}
}