using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface IFileInfoRepositoryService
	{
		FileInfo   GetByID(int      ID);
		FileInfo   GetByHash(string Hash);
		FileInfo[] GetAll();
	}
}