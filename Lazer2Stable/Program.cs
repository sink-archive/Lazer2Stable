using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lazer2Stable.Domain;

namespace Lazer2Stable
{
	internal static class Program
	{
		private static void Main()
		{
			PrintProgressInfo("Connecting to osu!lazer database (client.db)... ");
			Dictionary<BeatmapSetInfo, BeatmapSetFileInfo[]> maps;
			Dictionary<SkinInfo, SkinFileInfo[]>             skins;
			
			using (var dbReader = new LazerDbReader())
			{
				PrintSuccess();
				PrintProgressInfo("Reading data from database... ");
				dbReader.ReadAll();
				(maps, skins) = (dbReader.Mapsets, dbReader.Skins);
				PrintSuccess();
			}

			PrintCounts(maps, skins, out var setCount);

			var exportPath     = GetAndPrepareExportPath();
			var lazerFilesPath = LazerFolderUtils.GetLazerFiles();

			var exporter = new Exporter(lazerFilesPath, maps, skins);

			Console.CursorVisible = false; // makes spinner look nicer
			
			PrintProgressInfo($"Exporting {setCount} beatmap sets - this WILL take a while... ");
			exporter.ExportMaps(Path.Combine(exportPath, "Songs"));
			
			PrintSuccess();
			PrintProgressInfo($"Exporting {skins.Count} skins - this WILL take a while... ");
			exporter.ExportSkins(Path.Combine(exportPath, "Skins"));
			
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

		private static void PrintCounts(Dictionary<BeatmapSetInfo, BeatmapSetFileInfo[]> maps,
										Dictionary<SkinInfo, SkinFileInfo[]>             skins,
										out int                                          setCount)
		{
			setCount = maps.Keys.Count;
			var setFileCount = maps.Select(pair => pair.Value.Length)
									   .Aggregate(0, (current, next) => current + next);
			var skinCount   = skins.Count;
			var skinFileCount = skins.Select(pair => pair.Value.Length)
										 .Aggregate(0, (current, next) => current + next);

			Console.WriteLine($@"Found:
 - {setCount} beatmapsets, with {setFileCount} files
 - {skinCount} skins with {skinFileCount} files");
		}
	}
}