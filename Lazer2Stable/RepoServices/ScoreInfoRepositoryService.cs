using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class ScoreInfoRepositoryService : RepositoryServicesBase, IScoreInfoRepositoryService
	{
		public ScoreInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public ScoreInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}

		public ScoreInfo GetByID(int ID)
			=> Session.Query<ScoreInfo>().FirstOrDefault(s => s.ID == ID);

		public ScoreInfo[] GetAll()
			=> Session.Query<ScoreInfo>().ToArray();
	}
}