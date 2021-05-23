namespace Lazer2Stable
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var dbReader = new LazerDbReader();
			dbReader.ReadAll();
			var (maps, setFiles, scores, replays, skins, skinFiles) = (
				dbReader.Maps,
				dbReader.SetFiles,
				dbReader.Scores,
				dbReader.Replays,
				dbReader.Skins,
				dbReader.SkinFiles
				);

			dbReader.Dispose();
		}
	}
}