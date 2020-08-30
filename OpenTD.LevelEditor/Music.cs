using System.Collections.Generic;
using System.Linq;

namespace OpenTD.LevelEditor
{
	public class Music
	{
		private static readonly Dictionary<string, string> FileNames = new Dictionary<string, string>
		{
			["BastardZone"] = "Bastard zone",
			["Frenetic"] = "Frenetic",
			["GroovyTower"] = "Groovy tower",
			["HexagonForce"] = "Hexagon force",
			["HiddenPath"] = "Hidden path",
			["KeepGoing"] = "Keep going",
			["RaceAroundTheDesert"] = "Race around the desert",
			["WelkinWing"] = "Welkin wing",
			["Wingless"] = "Wingless"
		};

		public static List<string> Names =>
			FileNames.Values.ToList();
	}
}