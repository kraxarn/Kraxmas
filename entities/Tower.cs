using Godot;

public class Tower : Area2D
{
    private Enemy target;
    
    public override void _Ready()
    {
        RotationDegrees = -90;
        
        Connect("body_entered", this, nameof(OnBodyEntered));
        Connect("body_exited", this, nameof(OnBodyExited));
    }

    public override void _Process(float delta)
    {
        if (target != null)
            LookAt(target.Position);
    }

    public void OnBodyEntered(Node node)
    {
        if (node is Enemy enemy)
            target = enemy;
    }

    public void OnBodyExited(Node node)
    {
        if (node == target)
            target = null;
    }
}
