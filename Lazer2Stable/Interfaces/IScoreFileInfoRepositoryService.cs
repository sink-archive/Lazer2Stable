using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface IScoreFileInfoRepositoryService
	{
		ScoreFileInfo   GetByID(int ID);
		ScoreFileInfo[] GetAll();
	}
}