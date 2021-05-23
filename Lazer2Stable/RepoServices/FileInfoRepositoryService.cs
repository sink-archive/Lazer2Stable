using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.Interfaces;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class FileInfoRepositoryService : RepositoryServicesBase, IFileInfoRepositoryService
	{
		public FileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public FileInfoRepositoryService(IRepositoryServices repositoryService) : base(repositoryService)
		{
		}

		public FileInfo GetByID(int ID)
			=> Session.Query<FileInfo>().FirstOrDefault(f => f.ID == ID);

		public FileInfo GetByHash(string Hash)
			=> Session.Query<FileInfo>().FirstOrDefault(f => f.Hash == Hash);

		public FileInfo[] GetAll()
			=> Session.Query<FileInfo>().ToArray();
	}
}