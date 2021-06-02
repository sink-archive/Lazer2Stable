using Lazer2Stable.DbClasses;

namespace Lazer2Stable.Classes
{
	public class SkinFile
	{
		public int    Id       { get; set; }
		public File   File     { get; set; }
		public string Filename { get; set; }
		public Skin   Skin     { get; set; }

		public SkinFile(SkinFileInfo dbObj, RepoService repo)
		{
			Id       = dbObj.ID;
			File     = repo.FileById(dbObj.FileInfoId);
			Filename = dbObj.Filename;
			Skin     = repo.SkinById(dbObj.SkinInfoId);
		}
	}
}