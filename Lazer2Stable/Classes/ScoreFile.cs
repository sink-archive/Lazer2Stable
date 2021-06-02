using Lazer2Stable.DbClasses;

namespace Lazer2Stable.Classes
{
	public class ScoreFile
	{
		public int    Id       { get; set; }
		public File   File     { get; set; }
		public string Filename { get; set; }

		public ScoreFile(ScoreFileInfo dbObj, RepoService repo)
		{
			Id       = dbObj.ID;
			File     = repo.FileById(dbObj.FileInfoId);
			Filename = dbObj.Filename;
		}
	}
}