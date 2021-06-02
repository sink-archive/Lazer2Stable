using Lazer2Stable.DbClasses;

namespace Lazer2Stable.Classes
{
	public class Mapset
	{
		public int         Id          { get; set; }
		public int?        OnlineSetId { get; set; }
		public MapMetadata Metadata    { get; set; }

		public Mapset(BeatmapSetInfo dbObj, RepoService repo)
		{
			Id          = dbObj.ID;
			OnlineSetId = dbObj.OnlineBeatmapSetID;
			Metadata    = repo.MapMetadataById(dbObj.MetadataId);
		}
	}
}