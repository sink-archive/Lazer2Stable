using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class ScoreFileInfoRepositoryService : RepositoryServicesBase
	{
		public ScoreFileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public ScoreFileInfo[] GetAll()
			=> Session.Query<ScoreFileInfo>().ToArray();
	}
}