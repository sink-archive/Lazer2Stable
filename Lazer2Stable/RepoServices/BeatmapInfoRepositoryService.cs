using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class BeatmapInfoRepositoryService : RepositoryServicesBase, IBeatmapInfoRepositoryService
	{
		public BeatmapInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public BeatmapInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}
		public BeatmapInfo GetByID(int ID)
			=> Session.Query<BeatmapInfo>().FirstOrDefault(b => b.ID == ID);

		public BeatmapInfo[] GetAll()
			=> Session.Query<BeatmapInfo>().ToArray();
	}
}