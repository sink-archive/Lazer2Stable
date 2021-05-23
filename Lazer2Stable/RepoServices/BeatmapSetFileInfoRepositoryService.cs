using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class BeatmapSetFileInfoRepositoryService : RepositoryServicesBase, IBeatmapSetFileInfoRepositoryService
	{
		public BeatmapSetFileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public BeatmapSetFileInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}

		public BeatmapSetFileInfo GetByID(int ID)
			=> Session.Query<BeatmapSetFileInfo>().FirstOrDefault(b => b.ID == ID);

		public BeatmapSetFileInfo GetByBeatmapSetID(int ID)
			=> Session.Query<BeatmapSetFileInfo>().FirstOrDefault(b => b.BeatmapSetInfoID == ID);

		public BeatmapSetFileInfo[] GetAll()
			=> Session.Query<BeatmapSetFileInfo>().ToArray();
	}
}