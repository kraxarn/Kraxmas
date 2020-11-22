using Godot;

public class Enemy : KinematicBody2D
{
	[Export] public PackedScene Explosion;
	
	private Path2D path;
	private PathFollow2D pathFollow;

	private const float TraverseTime = 15;
	public float Progress { get; private set; }
	private float pathLength;

	private int health = 1;

	private Main Parent => GetParent<Main>();

	public int Health
	{
		get => health;
		set
		{
			health = value;
			if (health > 0)
				return;

			Parent.Hud.Money += 10;
			Explode();
			QueueFree();
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
		if (Progress > TraverseTime)
		{
			Parent.EnemyHit();
			QueueFree();
		}

		Progress += delta;

		pathFollow.Offset = Progress / TraverseTime * pathLength;
		Position = pathFollow.Position;
		Rotation = pathFollow.Rotation;
	}

	private void Explode()
	{
		var particles = (Particles2D)Explosion.Instance();
		particles.GlobalPosition = GlobalPosition;
		Parent.AddChild(particles);
		particles.OneShot = true;
		particles.Emitting = true;
	}
}