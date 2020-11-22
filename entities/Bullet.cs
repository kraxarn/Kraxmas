using Godot;

public class Bullet : RigidBody2D
{
	[Export] public int Speed = 500;

	public override void _Ready()
	{
		GetNode<VisibilityNotifier2D>(nameof(VisibilityNotifier2D))
			.Connect("screen_exited", this, nameof(OnScreenExited));

		Connect("body_entered", this, nameof(OnBodyEntered));
		GetNode<Area2D>(nameof(Area2D)).Connect("body_entered", this, nameof(OnBodyEntered));
	}

	public void Fire(Node2D parent, Vector2 target)
	{
		GlobalPosition = parent.GlobalPosition;
		Rotation = parent.Rotation;
		LinearVelocity = (target - GlobalPosition).Normalized() * Speed;
	}

	public void OnScreenExited()
	{
		QueueFree();
	}

	public void OnBodyEntered(Node node)
	{
		if (!(node is Enemy enemy))
			return;

		enemy.Health--;
		QueueFree();
	}
}