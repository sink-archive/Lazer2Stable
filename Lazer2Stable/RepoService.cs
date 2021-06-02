using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Lazer2Stable.Classes;
using SQLinq.Dapper;
using SQLinq.Dynamic;

namespace Lazer2Stable
{
	public class RepoService
	{
		#region Misc
		private readonly IDbConnection _connection;
		public RepoService(string dbLocation)
		{
			_connection = new SQLiteConnection($"Data Source={dbLocation};Version=3");
		}

		public File FileById(int id) => new(_connection.Query(new DynamicSQLinq("FileInfo")
																 .Where<int>("ID", i => i == id))
													   .First());
		#endregion
	
		#region Beatmaps

		public MapMetadata MapMetadataById(int id) => new(_connection.Query(new DynamicSQLinq("BeatmapMetadata")
																			   .Where<int>("ID", i => i == id))
																	 .First());
		public Mapset      MapsetById(int id) => new(_connection.Query(new DynamicSQLinq("BeatmapSetInfo")
																		  .Where<int>("ID", i => i == id))
																.First(), this);
		
		public Dictionary<Mapset, MapsetFile[]> BeatmapsGroupedBySet()
			=> _connection.Query(new DynamicSQLinq("BeatmapSetFileInfo"))
						  .GroupBy(sf => MapsetById(sf.BeatmapSetInfoId),
								   (set, files) => new KeyValuePair<Mapset, MapsetFile[]>
									   (set, files.Select(f => new MapsetFile(f, this)).ToArray()))
						  .ToDictionary(pair => pair.Key, pair => pair.Value);
		#endregion
		
		#region Scores
		public ScoreFile[] AllScores() => _connection.Query(new DynamicSQLinq("ScoreFileInfo"))
													 .Select(f => new ScoreFile(f, this))
													 .ToArray();

		#endregion
		
		#region Skins
		public Skin SkinById(int id) => new(_connection.Query(new DynamicSQLinq("SkinInfo")
																 .Where<int>("ID", i => i == id))
													   .First());
		
		public Dictionary<Skin, SkinFile[]> SkinFilesGroupedBySkin()
			=> _connection.Query(new DynamicSQLinq("SkinFileInfo"))
						  .GroupBy(sf => SkinById(sf.SkinInfoId),
								   (set, files) => new KeyValuePair<Skin, SkinFile[]>
									   (set, files.Select(f => new SkinFile(f, this)).ToArray()))
						  .ToDictionary(pair => pair.Key, pair => pair.Value);
		#endregion
	}
}