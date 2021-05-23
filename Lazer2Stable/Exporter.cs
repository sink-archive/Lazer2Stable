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
		public readonly string                                LazerFilesPath;
		public          BeatmapInfo[]                         Maps;
		public          Dictionary<int, BeatmapSetFileInfo[]> SetFiles;
		public Exporter(string lazerFilesPath, BeatmapInfo[] maps, Dictionary<int, BeatmapSetFileInfo[]> setFiles)
		{
			LazerFilesPath = lazerFilesPath;
			Maps           = maps;
			SetFiles       = setFiles;
		}

		public void ExportMaps(string outputPath)
		{
			var seenSets = new HashSet<int>();
			foreach (var map in Maps)
			{
				if (seenSets.Contains(map.BeatmapSetInfoID))
					continue;
				seenSets.Add(map.BeatmapSetInfoID);
				
				var folderName = map.BeatmapSetInfoID.ToString(); // TODO: setup the metadata and construct an authentic path name here
				Directory.CreateDirectory(Path.Combine(outputPath, folderName));
				
				foreach (var file in SetFiles[map.BeatmapSetInfoID])
				{
					var hash   = file.FileInfo.Hash;
					var sourceFolder = Path.Combine(LazerFilesPath, $"{hash[0]}/{hash[Range.EndAt(2)]}");
					var sourceFile = new DirectoryInfo(sourceFolder).GetFiles()
																		.FirstOrDefault(f => f.Name.StartsWith(hash));
					var sourcePath = sourceFile?.FullName;
					if (sourcePath == null) throw new Exception($"Failed to find file with hash {hash} on disk.");

					var destPath = Path.Combine(outputPath, folderName, file.Filename);
					// create directory to stop stuff like storyboards causing an epic fail (aka DirectoryNotFoundException)
					Directory.CreateDirectory(new FileInfo(destPath).DirectoryName ?? string.Empty);
					File.Copy(sourcePath, destPath);
				}
			}
		}
	}
}