using Lazer2Stable.DbClasses;

namespace Lazer2Stable.Classes
{
	public class MapsetFile
	{
		public int    Id       { get; set; }
		public Mapset Set      { get; set; }
		public File   File     { get; set; }
		public string Filename { get; set; }

		public MapsetFile(BeatmapSetFileInfo dbObj, RepoService repo)
		{
			Id       = dbObj.ID;
			Set      = repo.MapsetById(dbObj.BeatmapSetInfoId);
			File     = repo.FileById(dbObj.FileInfoId);
			Filename = dbObj.Filename;
		}
	}
}