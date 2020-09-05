using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace OpenTD.LevelEditor
{
	public class Music
	{
		private readonly ContentManager contentManager;

		public readonly Dictionary<string, string> FileNames;

		public Music(Game game)
		{
			contentManager = game.Content;

			FileNames = new Dictionary<string, string>();

			foreach (var fileName in Directory
				.EnumerateFiles(Path.Combine(contentManager.RootDirectory, "Music"), "*.ogg"))
			{
				var name = Path.GetFileNameWithoutExtension(fileName);
				var trackName = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
				trackName = $"{char.ToUpper(trackName[0])}{trackName[1..].ToLower()}";

				FileNames[name] = trackName;
			}
		}

		public KeyValuePair<string, string> RandomName =>
			FileNames.ToArray()[new Random().Next(FileNames.Count)];

		public TimeSpan Play(string track)
		{
			var song = contentManager.Load<Song>($"Music/{track}");
			if (song == null)
				return TimeSpan.Zero;

			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 1f;
			MediaPlayer.Play(song);

			return song.Duration;
		}
	}
}