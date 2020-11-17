using Godot;

public class Main : Node
{
	[Export] public PackedScene Enemy;
	[Export] public PackedScene Tower;

	private TextureRect selection;
	private Label debugInfo;
	private Timer enemyTimer;
	private ColorRect cover;

	private int health = 100;

	public override void _Ready()
	{
		selection = GetNode<TextureRect>("Selection");
		debugInfo = GetNode<Label>("DebugInfo");
		cover = GetNode<ColorRect>("Cover");

		enemyTimer = GetNode<Timer>("EnemyTimer");
		enemyTimer.Connect("timeout", this, nameof(OnEnemyTimerTimeout));
	}

	public override void _Process(float delta)
	{
		debugInfo.Text = $"FPS: {Engine.GetFramesPerSecond()}\nHP: {health}";
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
			if (mouseButton.Pressed)
			{
				var tower = (Tower) Tower.Instance();
				tower.Position = selection.RectPosition + selection.RectSize / 2;
				AddChild(tower);
			}
		}
	}

	public void OnEnemyTimerTimeout()
	{
		if (enemyTimer.WaitTime > 0.25)
			enemyTimer.WaitTime *= 0.98f;

		var enemy = (Enemy) Enemy.Instance();
		AddChild(enemy);

		enemy.Connect("Hit", this, nameof(OnEnemyHit));
	}

	public void OnEnemyHit()
	{
		if (health <= 0)
		{
			cover.Show();
			enemyTimer.Stop();
			return;
		}
		health -= 5;
	}
}