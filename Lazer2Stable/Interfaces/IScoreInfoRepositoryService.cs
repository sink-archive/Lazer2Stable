using Lazer2Stable.Domain;

namespace Lazer2Stable.Interfaces
{
	public interface IScoreInfoRepositoryService
	{
		ScoreInfo   GetByID(int ID);
		ScoreInfo[] GetAll();
	}
}