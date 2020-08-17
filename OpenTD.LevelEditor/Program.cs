using System;

namespace OpenTD.LevelEditor
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var game = new LevelEditor();
            game.Run();
        }
    }
}
