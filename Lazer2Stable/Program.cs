using System;
using System.Collections.Generic;
using System.IO;
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
			
			PrintCounts(maps, setFiles, scores, replays, skins, skinFiles, out var setCount);

			var exportPath     = GetAndPrepareExportPath();
			var lazerFilesPath = LazerFolderUtils.GetLazerFiles();

			var exporter = new Exporter(lazerFilesPath, maps, setFiles, skins, skinFiles, replays);

			Console.Write($"Exporting {setCount} beatmap sets - this WILL take a while... ");
			exporter.ExportMaps(Path.Combine(exportPath, "Songs"));
			
			Console.Write($"Done!\nExporting {skins.Length} skins - this WILL take a while... ");
			exporter.ExportSkins(Path.Combine(exportPath, "Skins"));
			
			Console.Write($"Done!\nExporting {replays.Length} replays... ");
			exporter.ExportReplays(Path.Combine(exportPath, "Replays"));
			
			Console.WriteLine("Done!");
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

		private static void PrintCounts(BeatmapInfo[] maps, Dictionary<int, BeatmapSetFileInfo[]> setFiles,
										ScoreInfo[] scores, ScoreFileInfo[] replays, SkinInfo[] skins,
										Dictionary<int, SkinFileInfo[]> skinFiles, out int setCount)
		{
			var mapCount = maps.Length;
			setCount = maps.Select(m => m.BeatmapSetInfoID)
							   .Distinct()
							   .Count();
			var setFileCount = setFiles.Select(pair => pair.Value.Length)
									   .Aggregate(0, (current, next) => current + next);
			var scoreCount  = scores.Length;
			var replayCount = replays.Length;
			var skinCount   = skins.Length;
			var skinFileCount = skinFiles.Select(pair => pair.Value.Length)
										 .Aggregate(0, (current, next) => current + next);

			Console.WriteLine("Found:\n"                                                                       +
							  $" - {mapCount} beatmaps in {setCount} beatmapsets, with {setFileCount} files\n" +
							  $" - {scoreCount} scores and {replayCount} replays\n"                            +
							  $" - {skinCount} skins with {skinFileCount} files");
		}
	}
}