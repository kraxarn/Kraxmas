using Godot;

public class Tower : Area2D
{
	[Export] public PackedScene Projectile;

	private Enemy target;

	private const float Cooldown = 1f;

	private float cooldown = Cooldown;

	public override void _Ready()
	{
		RotationDegrees = -90;

		Connect("body_entered", this, nameof(OnBodyEntered));
		Connect("body_exited", this, nameof(OnBodyExited));
	}

	public override void _Process(float delta)
	{
		cooldown -= delta;

		if (target == null)
			return;
		LookAt(target.Position);

		if (cooldown <= 0)
		{
			cooldown = Cooldown;
			var bullet = (Bullet) Projectile.Instance();
			GetParent<Main>().AddChild(bullet);
			bullet.Fire(this, target.GlobalPosition);
		}
	}

	public void OnBodyEntered(Node node)
	{
		if (node is Enemy enemy && (target == null || enemy.Progress > target.Progress))
			target = enemy;
	}

	public void OnBodyExited(Node node)
	{
		if (node == target)
			target = null;
	}
}