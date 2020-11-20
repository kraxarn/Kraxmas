using Godot;

public class Enemy : KinematicBody2D
{
	[Signal]
	public delegate void Hit();

	private Path2D path;
	private PathFollow2D pathFollow;

	private const float TraverseTime = 15;
	private float t;
	private float pathLength;

	private int health = 1;

	public int Health
	{
		get => health;
		set
		{
			health = value;
			if (health > 0)
				return;

			QueueFree();
			GetParent<Main>().Hud.Money += 10;
		}
	}

	public override void _Ready()
	{
		path = GetNode<Path2D>("../EnemyPath");
		pathLength = path.Curve.GetBakedLength();

		pathFollow = new PathFollow2D();
		path.AddChild(pathFollow);
	}

	public override void _Process(float delta)
	{
		if (t > TraverseTime)
		{
			EmitSignal("Hit");
			QueueFree();
		}

		t += delta;

		pathFollow.Offset = t / TraverseTime * pathLength;
		Position = pathFollow.Position;
		Rotation = pathFollow.Rotation;
	}
}