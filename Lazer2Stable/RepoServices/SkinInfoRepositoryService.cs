using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class SkinInfoRepositoryService : RepositoryServicesBase, ISkinInfoRepositoryService
	{
		public SkinInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public SkinInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}

		public SkinInfo GetByID(int ID)
			=> Session.Query<SkinInfo>().FirstOrDefault(s => s.ID == ID);

		public SkinInfo[] GetAll()
			=> Session.Query<SkinInfo>().ToArray();
	}
}