using Godot;

public class Main : Node
{
	private TextureRect selection;
	private Label debugInfo;

	private Node[,] towers;

	public override void _Ready()
	{
		selection = GetNode<TextureRect>("Selection");
		debugInfo = GetNode<Label>("DebugInfo");
		
		GD.Print($"{GetViewport().Size.x}x{GetViewport().Size.y}");
		var viewport = GetViewport();
		towers = new Node[(int) (viewport.Size.x / 64),(int) (viewport.Size.y / 64)];
		GD.Print($"Assuming {viewport.Size.x / 64}x{viewport.Size.y / 64} grid");
	}

	public override void _Process(float delta)
	{
		debugInfo.Text = $"FPS: {Engine.GetFramesPerSecond()}";
	}

	public override void _Input(InputEvent input)
	{
		if (input is InputEventMouseMotion mouseMotion)
		{
			var center = mouseMotion.Position - selection.RectSize / 2;
			selection.RectPosition = new Vector2(Mathf.Round(center.x / 64) * 64, Mathf.Round(center.y / 64) * 64);
		}
	}
}