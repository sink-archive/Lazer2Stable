namespace Lazer2Stable.Domain
{
	public class SkinFileInfo
	{
		public virtual int      ID       { get; set; }
		public virtual FileInfo FileInfo { get; set; }
		public virtual string   Filename { get; set; }
		public virtual SkinInfo SkinInfo { get; set; }
	}
}