using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class SkinFileInfoRepositoryService : RepositoryServicesBase, ISkinFileInfoRepositoryService
	{
		public SkinFileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public SkinFileInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}

		public SkinFileInfo GetByID(int ID)
			=> Session.Query<SkinFileInfo>().FirstOrDefault(s => s.ID == ID);

		public SkinFileInfo[] GetBySkinID(int ID)
			=> Session.Query<SkinFileInfo>().Where(s => s.SkinInfo.ID == ID).ToArray();

		public SkinFileInfo[] GetAll()
			=> Session.Query<SkinFileInfo>().ToArray();
	}
}