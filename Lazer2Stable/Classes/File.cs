namespace Lazer2Stable.Classes
{
	public class File
	{
		public int    Id             { get; set; }
		public string Hash           { get; set; }
		public int    ReferenceCount { get; set; }

		public File(DbClasses.FileInfo dbObj)
		{
			Id             = dbObj.ID;
			Hash           = dbObj.Hash;
			ReferenceCount = dbObj.ReferenceCount;
		}
	}
}