using System;
using System.Collections.Generic;
using System.Linq;
using Lazer2Stable.Domain;

namespace Lazer2Stable
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			Console.Write("Connecting to osu!lazer database (client.db)... ");
			var dbReader = new LazerDbReader();
			Console.Write("Done!\nReading data from database... ");
			dbReader.ReadAll();
			Console.WriteLine("Done!");
			var (maps, setFiles, scores, replays, skins, skinFiles) = (
				dbReader.Maps,
				dbReader.SetFiles,
				dbReader.Scores,
				dbReader.Replays,
				dbReader.Skins,
				dbReader.SkinFiles
				);
			dbReader.Dispose();
			
			PrintCounts(maps, setFiles, scores, replays, skins, skinFiles);
		}

		private static void PrintCounts(BeatmapInfo[] maps, Dictionary<int, BeatmapSetFileInfo[]> setFiles,
										ScoreInfo[] scores, ScoreFileInfo[] replays, SkinInfo[] skins,
										Dictionary<int, SkinFileInfo[]> skinFiles)
		{
			var mapCount = maps.Length;
			var setCount = maps.Select(m => m.BeatmapSetInfoID)
							   .Distinct()
							   .Count();
			var setFileCount = setFiles.Select(pair => pair.Value.Length)
									   .Aggregate(0, (current, next) => current + next);
			var scoreCount  = scores.Length;
			var replayCount = replays.Length;
			var skinCount   = skins.Length;
			var skinFileCount = skinFiles.Select(pair => pair.Value.Length)
										 .Aggregate(0, (current, next) => current + next);
			
			Console.WriteLine("Found:\n"                                                                      + 
							  $" - {mapCount} beatmaps in {setCount} beatmapsets, with {setFileCount} files\n" +
							  $" - {scoreCount} scores and {replayCount} replays\n"                            +
							  $" - {skinCount} skins with {skinFileCount} files");
		}
	}
}