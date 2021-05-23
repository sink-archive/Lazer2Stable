using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface IBeatmapSetFileInfoRepositoryService
	{
		BeatmapSetFileInfo GetByID(int           ID);
		BeatmapSetFileInfo GetByBeatmapSetID(int ID);
		BeatmapSetFileInfo[] GetAll();
	}
}