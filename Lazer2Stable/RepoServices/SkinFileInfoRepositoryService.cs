using System.Collections.Generic;
using System.Linq;
using Lazer2Stable.Domain;
using Lazer2Stable.RepoServiceBoilerplate;

namespace Lazer2Stable.RepoServices
{
	public class SkinFileInfoRepositoryService : RepositoryServicesBase
	{
		public SkinFileInfoRepositoryService(INHibernateSessionManager sessionManager) : base(sessionManager)
		{
		}

		public Dictionary<SkinInfo, SkinFileInfo[]> GetGroupedBySkin()
			=> Session.Query<SkinFileInfo>()
					  .GroupBy(sf => sf.SkinInfo,
							   (set, files) => new KeyValuePair<SkinInfo, SkinFileInfo[]>(set, files.ToArray()))
					  .ToDictionary(pair => pair.Key, pair => pair.Value);
	}
}