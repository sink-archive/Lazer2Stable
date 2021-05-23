using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface IBeatmapInfoRepositoryService
	{
		BeatmapInfo   GetByID(int ID);
		BeatmapInfo[] GetAll();
	}
}