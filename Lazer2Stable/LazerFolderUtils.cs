using System;
using System.IO;

namespace Lazer2Stable
{
	public static class LazerFolderUtils
	{
		public static string GetLazerFolder() 
			=> Path.Combine(Environment.OSVersion.Platform switch
		{
			PlatformID.Win32NT      => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			PlatformID.Unix         => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share"),
			PlatformID.MacOSX       => throw new NotImplementedException("MacOS is not supported yet!"),
			_                       => throw new ArgumentOutOfRangeException()
		}, "osu");

		public static string GetLazerDatabase()
			=> Path.Combine(GetLazerFolder(), "client.db");

		public static string GetLazerFiles()
			=> Path.Combine(GetLazerFolder(), "files");
	}
}