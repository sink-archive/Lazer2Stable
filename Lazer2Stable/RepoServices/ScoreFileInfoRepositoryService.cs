using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class ScoreFileInfoRepositoryService : RepositoryServicesBase, IScoreFileInfoRepositoryService
	{
		public ScoreFileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public ScoreFileInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}

		public ScoreFileInfo GetByID(int ID)
			=> Session.Query<ScoreFileInfo>().FirstOrDefault(s => s.ID == ID);

		public ScoreFileInfo[] GetAll()
			=> Session.Query<ScoreFileInfo>().ToArray();
	}
}