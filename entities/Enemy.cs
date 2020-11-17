using Godot;

public class Enemy : RigidBody2D
{
	[Signal]
	public delegate void Hit();
	
	private Path2D path;
	private PathFollow2D pathFollow;

	private float traverseTime = 15;
	private float t = 0;
	private float pathLength;

	public override void _Ready()
	{
		path = GetNode<Path2D>("../EnemyPath");
		pathLength = path.Curve.GetBakedLength();

		pathFollow = new PathFollow2D();
		path.AddChild(pathFollow);
	}

	public override void _Process(float delta)
	{
		if (t > traverseTime)
		{
			EmitSignal("Hit");
			QueueFree();
		}
		t += delta;

		pathFollow.Offset = t / traverseTime * pathLength;
		Position = pathFollow.Position;
		Rotation = pathFollow.Rotation;
	}
}