using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface ISkinInfoRepositoryService
	{
		SkinInfo   GetByID(int ID);
		SkinInfo[] GetAll();
	}
}