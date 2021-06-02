using System.Collections.Generic;
using Lazer2Stable.Classes;

namespace Lazer2Stable
{
	public class LazerDbReader
	{
		private RepoService _repo;

		public LazerDbReader() => _repo = new(LazerFolderUtils.GetLazerDatabase());

		public Dictionary<Mapset, MapsetFile[]> Mapsets { get; private set; } = new();

		public void ReadAllMaps() => Mapsets = _repo.BeatmapsGroupedBySet();

		public ScoreFile[] Scores          { get; private set; }
		public void            ReadAllScores() => Scores = _repo.AllScores();

		public Dictionary<Skin, SkinFile[]> Skins { get; private set; } = new();

		public void ReadAllSkins() => Skins = _repo.SkinFilesGroupedBySkin();

		public void ReadAll()
		{
			ReadAllMaps();
			ReadAllScores();
			ReadAllSkins();
		}
	}
}