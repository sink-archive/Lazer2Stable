using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface ISkinFileInfoRepositoryService
	{
		SkinFileInfo   GetByID(int ID);
		SkinFileInfo[] GetAll(); // THOUSANDS of objects returned from a single function go BRRRR
	}
}