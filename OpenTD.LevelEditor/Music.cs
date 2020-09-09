using System;
using System.Collections.Generic;
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

		public readonly HashSet<string> FileNames;

		public Music(Game game)
		{
			contentManager = game.Content;

			FileNames = new HashSet<string>
			{
				"Caketown",
				"Snowland"
			};
		}

		public (string id, string name) RandomName
		{
			get
			{
				var randomName = FileNames.ToArray()[new Random().Next(FileNames.Count)];
				return (randomName, FormatName(randomName));
			}
		}

		public static string FormatName(string name)
		{
			name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
			name = $"{char.ToUpper(name[0])}{name[1..].ToLower()}";
			return name;
		}

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