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

	public override void _Ready()
	{
		selection = GetNode<TextureRect>("Selection");
		cover = GetNode<ColorRect>("Cover");
		pause = GetNode<Pause>(nameof(Pause));
		Hud = GetNode<Hud>("Hud");

		enemyTimer = GetNode<Timer>("EnemyTimer");
		enemyTimer.Connect("timeout", this, nameof(OnEnemyTimerTimeout));

		//GetTree().DebugCollisionsHint = true;
	}

	public override void _Process(float delta)
	{
		Hud.DebugInfo = $"FPS: {Engine.GetFramesPerSecond()}";

		if (Input.IsActionJustPressed("ui_cancel"))
			pause.PopupCentered();
	}

	public override void _Input(InputEvent input)
	{
		if (input is InputEventMouseMotion mouseMotion)
		{
			var center = mouseMotion.Position - selection.RectSize / 2;
			selection.RectPosition = new Vector2(Mathf.Round(center.x / 64) * 64, Mathf.Round(center.y / 64) * 64);
		}
		else if (input is InputEventMouseButton mouseButton)
		{
			if (mouseButton.Pressed && Hud.Money >= 50)
			{
				var tower = (Tower) Tower.Instance();
				tower.Position = selection.RectPosition + selection.RectSize / 2;
				AddChild(tower);
				Hud.Money -= 50;
			}
		}
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
}