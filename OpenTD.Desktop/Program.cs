using System;
using OpenTD.Core;

namespace OpenTD.Desktop
{
	public static class Program
	{
		[STAThread]
		private static void Main()
		{
			using var game = new MainGame();
			game.Run();
		}
	}
}