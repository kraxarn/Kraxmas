using System;
using System.Linq;
using Godot;
using OpenTD;

public class Main : SceneBase
{
	[Export] public PackedScene Tower;

	private TextureRect selection;
	private ColorRect cover;

	private Pause pause;
	private TileMap currentMap;
	private MessageScreen messageScreen;

	private bool debugMode;

	private bool DebugMode
	{
		get => debugMode;
		set
		{
			Hud.DebugInfo = string.Empty;
			var tree = GetTree();
			tree.DebugCollisionsHint = value;
			tree.DebugNavigationHint = value;
			debugMode = value;
		}
	}

	private int totalEnemies;

	protected override int TotalEnemies
	{
		get => totalEnemies;
		set
		{
			Hud.Level = Mathf.FloorToInt(value / 10f + 1);
			totalEnemies = value;
		}
	}

	public int Money
	{
		get => Hud.Money;
		set
		{
			selection.Modulate = value < 50
				? Colors.Unavailable
				: Colors.Available;
			Hud.Money = value;
		}
	}

	private Vector2 Center => GetViewport().Size / 2;

	public override void _Ready()
	{
		selection = GetNode<TextureRect>("Selection");
		cover = GetNode<ColorRect>("Cover");
		pause = GetNode<Pause>(nameof(Pause));
		Hud = GetNode<Hud>("Hud");
		Music = GetNode<AudioStreamPlayer>("Music");
		messageScreen = GetNode<MessageScreen>("MessageScreen");

		SoundEffects = Enum
			.GetValues(typeof(SoundEffect))
			.Cast<SoundEffect>()
			.ToDictionary(k => k,
				v => GetNode<AudioStreamPlayer2D>($"Sound/{v}"));

		EnemyTimer = GetNode<Timer>("EnemyTimer");
		EnemyTimer.Connect("timeout", this, nameof(OnEnemyTimerTimeout));

		DebugMode = false;

		var config = new Config();
		SetMusicVolume(config.MusicVolume);
		SetSoundVolume(config.SoundVolume);
		Music.Play();

		LoadMap("Map1");
	}

	public override void _Process(float delta)
	{
		if (DebugMode)
			Hud.DebugInfo = $"FPS: {Engine.GetFramesPerSecond()}";

		if (Input.IsActionJustPressed("ui_cancel"))
			Pause(true);
		if (Input.IsActionJustPressed("ui_debug"))
			DebugMode = !DebugMode;
	}

	public override void _Input(InputEvent input)
	{
		if (input is InputEventMouseMotion mouseMotion)
		{
			var center = mouseMotion.Position - selection.RectSize / 2;
			selection.RectPosition = new Vector2(Mathf.Round(center.x / 64) * 64, Mathf.Round(center.y / 64) * 64);
		}
		else if (input is InputEventMouseButton mouseButton
		         && mouseButton.Pressed
		         && mouseButton.ButtonIndex == (int) ButtonList.Left)
		{
			PlaceTower(selection.RectPosition);
		}
		else if (input is InputEventScreenTouch screenTouch)
		{
			PlaceTower(screenTouch.Position);
		}
	}

	private void PlaceTower(Vector2 position)
	{
		if (Money < 50)
		{
			PlaySound(SoundEffect.Error, position);
			return;
		}

		var tower = (Tower) Tower.Instance();
		tower.Position = position + selection.RectSize / 2;
		AddChild(tower);
		Money -= 50;
	}

	public override void OnEnemyTimerTimeout()
	{
		if (Hud.Level > Hud.MaxLevel)
		{
			if (GetTree().GetNodesInGroup("enemies").Count <= 0)
			{
				ShowMessageScreen(MessageType.Victory);
			}

			return;
		}

		if (EnemyTimer.WaitTime > 0.25)
			EnemyTimer.WaitTime *= 0.98f;

		base.OnEnemyTimerTimeout();
	}

	public void EnemyHit()
	{
		if (Hud.Health <= 0)
		{
			ShowMessageScreen(MessageType.GameOver);
			return;
		}

		PlaySound(SoundEffect.Hurt);
		Hud.Health -= 5;
	}

	public void ShowMessageScreen(MessageType messageType)
	{
		cover.Show();
		GetTree().Paused = true;

		PlaySound(messageType == MessageType.Victory
			? SoundEffect.Upgrade
			: SoundEffect.GameOver);
		messageScreen.Show(messageType);
	}

	public void PlaySound(SoundEffect sfx, Vector2? pos = null)
	{
		var audio = SoundEffects[sfx];
		if (audio == null)
			return;

		audio.Position = pos?.MoveToward(Center, 250) ?? Center;
		audio.Play();
	}

	public override void Pause(bool paused)
	{
		GetTree().Paused = paused;
		cover.Visible = paused;
		pause.Visible = paused;
	}

	public void LoadMap(string name)
	{
		currentMap?.QueueFree();
		currentMap = (TileMap) ((PackedScene) ResourceLoader.Load($"res://maps/{name}.tscn")).Instance();
		currentMap.ZIndex = -1;
		AddChild(currentMap);
	}
}