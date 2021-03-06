using System;
using System.Collections.Generic;
using Lazer2Stable.Domain;
using Lazer2Stable.RepoServiceBoilerplate;
using Lazer2Stable.RepoServices;

namespace Lazer2Stable
{
	public class LazerDbReader : IDisposable
	{
		private readonly NHibernateSessionManager            _sessionManager;
		private          BeatmapSetFileInfoRepositoryService _beatmapSetFileRepo;
		private          SkinFileInfoRepositoryService       _skinFileRepo;

		public LazerDbReader() => _sessionManager = new NHibernateSessionManager(LazerFolderUtils.GetLazerDatabase());

		public Dictionary<BeatmapSetInfo, BeatmapSetFileInfo[]> Mapsets { get; private set; } = new();

		public void ReadAllMaps()
		{
			_beatmapSetFileRepo = new(_sessionManager);

			Mapsets = _beatmapSetFileRepo.GetGroupedBySet();
			
			_beatmapSetFileRepo.Dispose();
		}

		public Dictionary<SkinInfo, SkinFileInfo[]> Skins { get; private set; } = new();

		public void ReadAllSkins()
		{
			_skinFileRepo = new(_sessionManager);

			Skins = _skinFileRepo.GetGroupedBySkin();
			
			_skinFileRepo.Dispose();
		}

		public void ReadAll()
		{
			ReadAllMaps();
			ReadAllSkins();
		}

		public void Dispose() => _sessionManager?.Dispose();
	}
}