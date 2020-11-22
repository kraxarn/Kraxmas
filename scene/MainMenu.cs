using Godot;
using System;

public class MainMenu : SceneBase
{
	protected override int TotalEnemies
	{
		get => 0;
		set { }
	}

	private Pause settings;

	public override void _Ready()
	{
		settings = GetNode<Pause>("Pause");
		settings.AcceptVisible = false;
		settings.TextColor = Godot.Colors.Black;

		GetNode<Timer>("EnemyTimer")
			.Connect("timeout", this, nameof(OnEnemyTimerTimeout));

		GetNode<TextureButton>("Control/Menu/StartGame")
			.Connect("pressed", this, nameof(StartGame));
		GetNode<TextureButton>("Control/Menu/Settings")
			.Connect("pressed", this, nameof(Settings));
		GetNode<TextureButton>("Control/Menu/QuitGame")
			.Connect("pressed", this, nameof(QuitGame));

		Music = GetNode<AudioStreamPlayer>("Music");
		var config = new Config();
		SetMusicVolume(config.MusicVolume * 0.75f);
		Music.Play();
	}

	public void StartGame()
	{
		GetTree().ChangeScene("res://scene/Main.tscn");
	}

	public void Settings()
	{
		if (settings.Visible)
			settings.Accept();

		settings.Visible = !settings.Visible;
	}

	public void QuitGame()
	{
		GetTree().Quit();
	}

	public override void Pause(bool paused)
	{
	}
}