using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Lazer2Stable.Classes;
using File = System.IO.File;
using FileInfo = System.IO.FileInfo;

namespace Lazer2Stable
{
	public class Exporter
	{
		public readonly string                           LazerFilesPath;
		public          Dictionary<Mapset, MapsetFile[]> Maps;
		public          Dictionary<Skin, SkinFile[]>     Skins;
		public          ScoreFile[]                      Replays;

		private readonly ImmutableDictionary<string, FileInfo> _sourceFiles;

		public Exporter(string                       lazerFilesPath, Dictionary<Mapset, MapsetFile[]> maps,
						Dictionary<Skin, SkinFile[]> skins,          ScoreFile[]                  replays)
		{
			LazerFilesPath = lazerFilesPath;
			Maps           = maps;
			Skins          = skins;
			Replays        = replays;

			_sourceFiles = new DirectoryInfo(lazerFilesPath)           // lazer files dir
						  .GetDirectories()                            // all dirs (a, b, c, etc)
						  .SelectMany(d => d.GetDirectories())         // get subdirs (a/aa, a/ab, a/ac, etc)
						  .SelectMany(d => d.GetFiles())               // get files (a/aa/*, etc)
						  .ToImmutableDictionary(f => f.Name, f => f); // enumerate to dictionary for performance later
		}

		public void ExportMaps(string outputPath)
		{
			Spinner.RunSpinner();
			foreach (var (set, files) in Maps)
			{
				var folderName = $"{set.OnlineSetId} {set.Metadata.Artist} - {set.Metadata.Title}";
				Directory.CreateDirectory(Path.Combine(outputPath, folderName));
				
				foreach (var file in files) CopyFile(outputPath, file, folderName, file.File.Id);
			}
			Spinner.StopAllSpinners();
		}

		public void ExportSkins(string outputPath)
		{
			Spinner.RunSpinner();
			foreach (var (skin, files) in Skins)
			{
				var folderName = skin.Name;
				Directory.CreateDirectory(Path.Combine(outputPath, folderName));

				foreach (var file in files) CopyFile(outputPath, file, folderName, file.File.Id);
			}
			Spinner.StopAllSpinners();
		}
		
		public void ExportReplays(string outputPath)
		{
			Spinner.RunSpinner();
			foreach (var replay in Replays)
			{
				Directory.CreateDirectory(outputPath);

				CopyFile(outputPath, replay, string.Empty, replay.Id);
			}
			Spinner.StopAllSpinners();
		}

		private void CopyFile(string outputPath, object file, string folderName, int id)
		{
			var hash = file switch
			{
				SkinFile sf   => sf.File.Hash,
				MapsetFile mf => mf.File.Hash,
				ScoreFile r   => r.File.Hash,
				_             => throw new ArgumentOutOfRangeException(nameof(file), file, null)
			};
			Path.Combine(LazerFilesPath, $"{hash[0]}/{hash[Range.EndAt(2)]}");
			var sourceFile   = _sourceFiles.ContainsKey(hash) ? _sourceFiles[hash] : null;
			var sourcePath   = sourceFile?.FullName;
			if (sourcePath == null) throw new Exception($"Failed to find file with hash {hash} on disk.");

#pragma warning disable 8509
			var fileName = file switch
#pragma warning restore 8509
			{
				SkinFile sf   => sf.Filename,
				MapsetFile bf => bf.Filename,
				ScoreFile r   => r.Filename
			};
			var destPath = Path.Combine(outputPath, folderName, /*file.Filename*/ fileName);
			// create directory to stop folders in skins causing epic fails
			Directory.CreateDirectory(new FileInfo(destPath).DirectoryName ?? string.Empty);
            // fix for issue with replays - multiple called "replay.osr" which causes issues.
            CheckAndFixConflicts(ref destPath, id);
			File.Copy(sourcePath, destPath);
		}

		private static void CheckAndFixConflicts(ref string destPath, int id)
		{
			if (File.Exists(destPath))
				destPath += $"_{id}";
		}
	}
}