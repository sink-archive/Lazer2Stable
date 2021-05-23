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
		public          SkinInfo[]                            Skins;
		public          Dictionary<int, SkinFileInfo[]>       SkinFiles;

		public Exporter(string     lazerFilesPath, BeatmapInfo[] maps, Dictionary<int, BeatmapSetFileInfo[]> setFiles,
						SkinInfo[] skins,          Dictionary<int, SkinFileInfo[]> skinFiles)
		{
			LazerFilesPath = lazerFilesPath;
			Maps           = maps;
			SetFiles       = setFiles;
			Skins          = skins;
			SkinFiles      = skinFiles;
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
				
				foreach (var file in SetFiles[map.BeatmapSetInfoID]) CopyFile(outputPath, file, folderName);
			}
		}

		public void ExportSkins(string outputPath)
		{
			foreach (var skin in Skins)
			{
				var folderName = skin.Name;
				Directory.CreateDirectory(Path.Combine(outputPath, folderName));

				foreach (var file in SkinFiles[skin.ID]) CopyFile(outputPath, file, folderName);
			}
		}

		private void CopyFile(string outputPath, object file, string folderName)
		{
			//var hash         = file.FileInfo.Hash;
			var hash = file switch
			{
				SkinFileInfo sf       => sf.FileInfo.Hash,
				BeatmapSetFileInfo bf => bf.FileInfo.Hash
			};
			var sourceFolder = Path.Combine(LazerFilesPath, $"{hash[0]}/{hash[Range.EndAt(2)]}");
			var sourceFile   = new DirectoryInfo(sourceFolder).GetFiles().FirstOrDefault(f => f.Name.StartsWith(hash));
			var sourcePath   = sourceFile?.FullName;
			if (sourcePath == null) throw new Exception($"Failed to find file with hash {hash} on disk.");

			var fileName = file switch
			{
				SkinFileInfo sf       => sf.Filename,
				BeatmapSetFileInfo bf => bf.Filename
			};
			var destPath = Path.Combine(outputPath, folderName, /*file.Filename*/ fileName);
			// create directory to stop folders in skins causing epic fails
			Directory.CreateDirectory(new FileInfo(destPath).DirectoryName ?? string.Empty);
			File.Copy(sourcePath, destPath);
		}
	}
}