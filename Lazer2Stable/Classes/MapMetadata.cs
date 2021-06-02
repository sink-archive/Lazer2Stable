namespace Lazer2Stable.Classes
{
	public class MapMetadata
	{
		public int    Id     { get; set; }
		public string Artist { get; set; }
		public string Title  { get; set; }

		public MapMetadata(DbClasses.BeatmapMetadata dbObj)
		{
			Id     = dbObj.ID;
			Artist = dbObj.Artist;
			Title  = dbObj.Title;
		}
	}
}