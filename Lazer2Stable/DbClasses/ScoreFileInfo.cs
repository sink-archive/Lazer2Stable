namespace Lazer2Stable.DbClasses
{
	public class ScoreFileInfo
	{
		public virtual int      ID         { get; set; }
		public virtual int      FileInfoId { get; set; }
		public virtual string   Filename   { get; set; }
	}
}