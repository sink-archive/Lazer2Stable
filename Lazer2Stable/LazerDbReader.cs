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
		private          BeatmapInfoRepositoryService        _beatmapRepo;
		private          BeatmapSetFileInfoRepositoryService _beatmapSetFileRepo;
		private          ScoreInfoRepositoryService          _scoreRepo;
		private          ScoreFileInfoRepositoryService      _replayRepo;
		private          SkinFileInfoRepositoryService       _skinFileRepo;
		private          SkinInfoRepositoryService           _skinRepo;

		public LazerDbReader()
		{
			_sessionManager = new NHibernateSessionManager("client.db");
		}

		public BeatmapInfo[]                         Maps     { get; private set; }
		public Dictionary<int, BeatmapSetFileInfo[]> SetFiles { get; } = new();

		public void ReadAllMaps()
		{
			_beatmapRepo = new(_sessionManager);
			Maps         = _beatmapRepo.GetAll();
			_beatmapRepo.Dispose();
		}

		public void ReadAllSetFiles()
		{
			_beatmapSetFileRepo = new(_sessionManager);
			var sets = new HashSet<int>();
			foreach (var map in Maps)
			{
				if (sets.Contains(map.BeatmapSetInfoID))
					continue;
				sets.Add(map.BeatmapSetInfoID);

				SetFiles[map.BeatmapSetInfoID] = _beatmapSetFileRepo.GetByBeatmapSetID(map.BeatmapSetInfoID);
			}

			_beatmapSetFileRepo.Dispose();
		}

		public ScoreInfo[]     Scores  { get; private set; }
		public ScoreFileInfo[] Replays { get; private set; }

		public void ReadAllScores()
		{
			_scoreRepo = new(_sessionManager);
			Scores     = _scoreRepo.GetAll();
			_scoreRepo.Dispose();
		}

		public void ReadAllReplays()
		{
			_replayRepo = new(_sessionManager);
			Replays     = _replayRepo.GetAll();
			_replayRepo.Dispose();
		}

		public SkinInfo[]                      Skins     { get; private set; }
		public Dictionary<int, SkinFileInfo[]> SkinFiles { get; } = new();

		public void ReadAllSkins()
		{
			_skinRepo = new(_sessionManager);
			Skins     = _skinRepo.GetAll();
			_skinRepo.Dispose();
		}

		public void ReadAllSkinFiles()
		{
			_skinFileRepo = new(_sessionManager);
			var skins = new HashSet<int>();
			foreach (var skin in Skins)
			{
				if (skins.Contains(skin.ID))
					continue;
				skins.Add(skin.ID);

				SkinFiles[skin.ID] = _skinFileRepo.GetBySkinID(skin.ID);
			}

			_skinFileRepo.Dispose();
		}

		public void ReadAll()
		{
			ReadAllMaps();
			ReadAllSetFiles();
			ReadAllScores();
			ReadAllReplays();
			ReadAllSkins();
			ReadAllSkinFiles();
		}

		public void Dispose() => _sessionManager?.Dispose();
	}
}