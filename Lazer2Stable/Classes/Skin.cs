using Lazer2Stable.DbClasses;

namespace Lazer2Stable.Classes
{
	public class Skin
	{
		public int    Id   { get; set; }
		public string Name { get; set; }

		public Skin(SkinInfo dbObj)
		{
			Id   = dbObj.ID;
			Name = dbObj.Name;
		}
	}
}