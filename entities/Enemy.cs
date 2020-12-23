using Godot;
using OpenTD;

public class Enemy : KinematicBody2D
{
	[Export] public PackedScene Explosion;

	private Path2D path;
	private PathFollow2D pathFollow;

	private const float TraverseTime = 15;
	public float Progress { get; private set; }
	private float pathLength;

	private int health = 1;

	public int Health
	{
		get => health;
		set
		{
			var main = GetParent() as Main;

			if (value < health)
				main?.PlaySound(value > 0 ? SoundEffect.Hit : SoundEffect.Explosion, Position);

			health = value;
			if (health > 0)
				return;

			if (main != null)
				main.Money += 10;
			Explode();
			QueueFree();
		}
	}

	public override void _Ready()
	{
		path = GetNode<Path2D>("../Map/EnemyPath");
		pathLength = path.Curve.GetBakedLength();

		pathFollow = new PathFollow2D();
		path.AddChild(pathFollow);
	}

	public override void _Process(float delta)
	{
		if (Progress > TraverseTime)
		{
			(GetParent() as Main)?.EnemyHit();
			QueueFree();
		}

		Progress += delta;

		pathFollow.Offset = Progress / TraverseTime * pathLength;
		Position = pathFollow.Position;
		Rotation = pathFollow.Rotation;
	}

	private void Explode()
	{
		var particles = (Particles2D) Explosion.Instance();
		particles.GlobalPosition = GlobalPosition;
		GetParent().AddChild(particles);
		particles.OneShot = true;
		particles.Emitting = true;
	}
}