using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lazer2Stable.Classes;
using File = System.IO.File;

namespace Lazer2Stable
{
	internal static class Program
	{
		private static void Main()
		{
			PrintProgressInfo("Connecting to osu!lazer database (client.db)... ");
			var dbReader = new LazerDbReader();
			PrintSuccess();
			PrintProgressInfo("Reading data from database... ");
			dbReader.ReadAll();
			PrintSuccess();
			var (maps, scores, skins) = (
				dbReader.Mapsets,
				dbReader.Scores,
				dbReader.Skins
				);

			PrintCounts(maps, scores, skins);

			var exportPath     = GetAndPrepareExportPath();
			var lazerFilesPath = LazerFolderUtils.GetLazerFiles();

			var exporter = new Exporter(lazerFilesPath, maps, skins, scores);

			Console.CursorVisible = false; // makes spinner look nicer
			
			PrintProgressInfo($"Exporting {maps.Keys.Count} mapsets - this WILL take a while... ");
			exporter.ExportMaps(Path.Combine(exportPath, "Songs"));
			
			PrintSuccess();
			PrintProgressInfo($"Exporting {skins.Count} skins - this WILL take a while... ");
			exporter.ExportSkins(Path.Combine(exportPath, "Skins"));
			
			PrintSuccess();
			PrintProgressInfo($"Exporting {scores.Length} score replays... ");
			exporter.ExportReplays(Path.Combine(exportPath, "Replays"));
			
			PrintSuccess();

			Console.CursorVisible = true; // dont make peoples terminals hard to use
		}

		private static void PrintProgressInfo(string info)
		{
			var col = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(info);
			Console.ForegroundColor = col;
		}

		private static void PrintSuccess(string success = "Done!")
		{
			var col = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(success);
			Console.ForegroundColor = col;
		}

		private static string GetAndPrepareExportPath()
		{
			reEnterPath:
			Console.Write("Enter a path to export to: ");
			var dirPath = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(dirPath))
			{
				Console.WriteLine("Please enter a path.");
				goto reEnterPath;
			}

			if (File.Exists(dirPath))
			{
				Console.WriteLine("That path is a file. Please enter a different path.");
				goto reEnterPath; // shut up ik this can be a loop but this is just easier
			}

			if (Directory.Exists(dirPath))
			{
				Console.WriteLine(!new DirectoryInfo(dirPath).EnumerateFileSystemInfos().Any()
									  ? "This directory exists but is empty. Press enter to export to it or ctrl-c to exit."
									  : "This directory is not empty. Exporting here is a really bad idea but if you want to hit enter, else ctrl-c to exit.");

				Console.ReadLine();
			}

			Directory.CreateDirectory(dirPath);

			return dirPath;
		}

		private static void PrintCounts(Dictionary<Mapset, MapsetFile[]> maps,
										ScoreFile[]                      scores,
										Dictionary<Skin, SkinFile[]>     skins)
		{
			var setCount = maps.Keys.Count;
			var setFileCount = maps.Select(pair => pair.Value.Length)
									   .Aggregate(0, (current, next) => current + next);
			var scoreCount  = scores.Length;
			var skinCount   = skins.Count;
			var skinFileCount = skins.Select(pair => pair.Value.Length)
										 .Aggregate(0, (current, next) => current + next);

			Console.WriteLine($@"Found:
 - {setCount} beatmapsets, with {setFileCount} files
 - {scoreCount} scores with exportable replays
 - {skinCount} skins with {skinFileCount} files");
		}
	}
}