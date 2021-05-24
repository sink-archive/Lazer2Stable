using System.Collections.Generic;
using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class BeatmapSetFileInfoRepositoryService : RepositoryServicesBase
	{
		public BeatmapSetFileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public Dictionary<BeatmapSetInfo, BeatmapSetFileInfo[]> GetGroupedBySet()
			=> Session.Query<BeatmapSetFileInfo>()
					  .GroupBy(sf => sf.BeatmapSetInfo,
							   (set, files) => new KeyValuePair<BeatmapSetInfo, BeatmapSetFileInfo[]>(set, files.ToArray()))
					  .ToDictionary(pair => pair.Key, pair => pair.Value);
	}
}