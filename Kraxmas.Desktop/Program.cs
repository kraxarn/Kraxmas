using System;
using Kraxmas.Core;

namespace Kraxmas.Desktop
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