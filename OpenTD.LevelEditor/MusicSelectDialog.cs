using System;
using System.Timers;
using Microsoft.Xna.Framework.Media;
using Myra.Graphics2D.UI;

namespace OpenTD.LevelEditor
{
	public class MusicSelectDialog : Dialog
	{
		public MusicSelectDialog(LevelEditor levelEditor)
		{
			Title = "Music";
			Width = 200;

			var music = levelEditor.Music;
			var (currentId, currentName) = Music.RandomName;

			var stack = new VerticalStackPanel
			{
				Spacing = 12
			};
			stack.Widgets.Add(new Label
			{
				Text = $"Current: {currentName}"
			});

			var comboBox = new ComboBox
			{
				HorizontalAlignment = HorizontalAlignment.Stretch
			};
			foreach (var (track, name) in Music.FileNames)
			{
				comboBox.Items.Add(new ListItem
				{
					Id = track.ToString(),
					Text = name,
					IsSelected = track == currentId
				});
			}

			comboBox.SelectedIndex = (int) currentId;
			stack.Widgets.Add(comboBox);
			
			var progressStack = new HorizontalStackPanel
			{
				Spacing = 12
			};
			
			var progress = new Label
			{
				Text = "0:00/0:00"
			};
			progressStack.Widgets.Add(progress);

			var timer = new Timer(1000)
			{
				AutoReset = true,
				Enabled = true
			};
			timer.Elapsed += (sender, args) =>
			{
				var current = MediaPlayer.PlayPosition;
				var p = progress.Text.Split('/');
				progress.Text = $"{FormatTimestamp(current)}/{p[1]}";
			};

			var playButton = new TextButton
			{
				Text = "Play preview"
			};
			playButton.Click += (sender, args) =>
				progress.Text = $"0:00/{FormatTimestamp(music.Play(comboBox.SelectedItem.Id))}";
			progressStack.Widgets.Add(playButton);
			
			stack.Widgets.Add(progressStack);
			Content = stack;
		}

		private string FormatTimestamp(TimeSpan duration)
		{
			var secs = duration.Seconds;
			return $"{Math.Floor(duration.TotalMinutes)}:{(secs < 10 ? "0" : "")}{secs}";
		}
	}
}