using Godot;

public class Main : Node
{
	[Export] public PackedScene Enemy;
	[Export] public PackedScene Tower;

	private TextureRect selection;
	private Timer enemyTimer;
	private ColorRect cover;

	private Pause pause;
	public Hud Hud { get; private set; }
	private AudioStreamPlayer music;

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

	private int TotalEnemies
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

	public override void _Ready()
	{
		selection = GetNode<TextureRect>("Selection");
		cover = GetNode<ColorRect>("Cover");
		pause = GetNode<Pause>(nameof(Pause));
		Hud = GetNode<Hud>("Hud");
		music = GetNode<AudioStreamPlayer>("Music");

		enemyTimer = GetNode<Timer>("EnemyTimer");
		enemyTimer.Connect("timeout", this, nameof(OnEnemyTimerTimeout));

		DebugMode = false;

		var config = new Config();
		SetMusicVolume(config.MusicVolume);
		music.Play();
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
		         && mouseButton.ButtonIndex == (int)ButtonList.Left)
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
			return;

		var tower = (Tower) Tower.Instance();
		tower.Position = position + selection.RectSize / 2;
		AddChild(tower);
		Money -= 50;
	}

	public void OnEnemyTimerTimeout()
	{
		if (Hud.Level > Hud.MaxLevel)
		{
			if (GetTree().GetNodesInGroup("enemies").Count <= 0)
			{
				GD.Print("You win!");
				enemyTimer.Stop();
			}

			return;
		}

		if (enemyTimer.WaitTime > 0.25)
			enemyTimer.WaitTime *= 0.98f;

		var enemy = (Enemy) Enemy.Instance();
		enemy.Health = Hud.Level;
		enemy.AddToGroup("enemies");
		AddChild(enemy);
		TotalEnemies++;
	}

	public void EnemyHit()
	{
		if (Hud.Health <= 0)
		{
			cover.Show();
			enemyTimer.Stop();
			return;
		}

		Hud.Health -= 5;
	}

	public void Pause(bool paused)
	{
		GetTree().Paused = paused;
		cover.Visible = paused;
		pause.Visible = paused;
	}

	public void SetMusicVolume(float value)
	{
		music.VolumeDb = Mathf.Log(value) * 20;
	}
}