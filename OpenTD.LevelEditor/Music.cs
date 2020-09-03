using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using OpenTD.LevelEditor.Enum;

namespace OpenTD.LevelEditor
{
	public class Music
	{
		private readonly ContentManager contentManager;

		public Music(LevelEditor levelEditor)
		{
			contentManager = levelEditor.Content;
		}

		public static readonly Dictionary<MusicTrack, string> FileNames = new Dictionary<MusicTrack, string>
		{
			[MusicTrack.BastardZone] = "Bastard zone",
			[MusicTrack.Frenetic] = "Frenetic",
			[MusicTrack.GroovyTower] = "Groovy tower",
			[MusicTrack.HexagonForce] = "Hexagon force",
			[MusicTrack.HiddenPath] = "Hidden path",
			[MusicTrack.KeepGoing] = "Keep going",
			[MusicTrack.RaceAroundTheDesert] = "Race around the desert",
			[MusicTrack.WelkinWing] = "Welkin wing",
			[MusicTrack.Wingless] = "Wingless"
		};

		public TimeSpan Play(string name) =>
			System.Enum.TryParse(name, out MusicTrack track)
				? Play(track)
				: TimeSpan.Zero;

		public TimeSpan Play(MusicTrack track)
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