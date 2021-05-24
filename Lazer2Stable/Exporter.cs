using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lazer2Stable.Domain;
using FileInfo = System.IO.FileInfo;

namespace Lazer2Stable
{
	public class Exporter
	{
		public readonly string                                           LazerFilesPath;
		public          Dictionary<BeatmapSetInfo, BeatmapSetFileInfo[]> Maps;
		public          Dictionary<SkinInfo, SkinFileInfo[]>             Skins;
		public          ScoreFileInfo[]                                  Replays;

		public Exporter(string     lazerFilesPath, Dictionary<BeatmapSetInfo, BeatmapSetFileInfo[]> maps,
						Dictionary<SkinInfo, SkinFileInfo[]> skins, ScoreFileInfo[] replays)
		{
			LazerFilesPath = lazerFilesPath;
			Maps           = maps;
			Skins          = skins;
			Replays   = replays;
		}

		public void ExportMaps(string outputPath)
		{
			foreach (var (set, files) in Maps)
			{
				var folderName = $"{set.OnlineBeatmapSetID} {set.Metadata.Artist} - {set.Metadata.Title}";
				Directory.CreateDirectory(Path.Combine(outputPath, folderName));
				
				foreach (var file in files) CopyFile(outputPath, file, folderName, file.FileInfo.ID);
			}
		}

		public void ExportSkins(string outputPath)
		{
			foreach (var (skin, files) in Skins)
			{
				var folderName = skin.Name;
				Directory.CreateDirectory(Path.Combine(outputPath, folderName));

				foreach (var file in files) CopyFile(outputPath, file, folderName, file.FileInfo.ID);
			}
		}
		
		public void ExportReplays(string outputPath)
		{
			foreach (var replay in Replays)
			{
				Directory.CreateDirectory(outputPath);

				CopyFile(outputPath, replay, string.Empty, replay.ID);
			}
		}

		private void CopyFile(string outputPath, object file, string folderName, int id)
		{
			var hash = file switch
			{
				SkinFileInfo sf       => sf.FileInfo.Hash,
				BeatmapSetFileInfo bf => bf.FileInfo.Hash,
				ScoreFileInfo r       => r.FileInfo.Hash,
				_                     => throw new ArgumentOutOfRangeException(nameof(file), file, null)
			};
			var sourceFolder = Path.Combine(LazerFilesPath, $"{hash[0]}/{hash[Range.EndAt(2)]}");
			var sourceFile   = new DirectoryInfo(sourceFolder).GetFiles().FirstOrDefault(f => f.Name.StartsWith(hash));
			var sourcePath   = sourceFile?.FullName;
			if (sourcePath == null) throw new Exception($"Failed to find file with hash {hash} on disk.");

#pragma warning disable 8509
			var fileName = file switch
#pragma warning restore 8509
			{
				SkinFileInfo sf       => sf.Filename,
				BeatmapSetFileInfo bf => bf.Filename,
				ScoreFileInfo r       => r.Filename
			};
			var destPath = Path.Combine(outputPath, folderName, /*file.Filename*/ fileName);
			// create directory to stop folders in skins causing epic fails
			Directory.CreateDirectory(new FileInfo(destPath).DirectoryName ?? string.Empty);
			// fix for issue with replays - multiple called "replay.osr" which causes issues.
			CheckAndFixConflicts(ref destPath, id);
			File.Copy(sourcePath, destPath);
		}

		private void CheckAndFixConflicts(ref string destPath, int id)
		{
			if (File.Exists(destPath))
				destPath += $"_{id}";
		}
	}
}