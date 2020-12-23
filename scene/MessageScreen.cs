using Godot;
using OpenTD;

public class MessageScreen : Control
{
	private Label title;
	private Label message;

	private TextureButton button;
	private Label buttonLabel;

	private Main Main => GetParent() as Main;

	private MessageType messageType;

	public override void _Ready()
	{
		title = GetNode<Label>("Grid/Title");
		message = GetNode<Label>("Grid/Message");
		buttonLabel = GetNode<Label>("Grid/Button/Label");

		button = GetNode<TextureButton>("Grid/Button");
		button.Connect("pressed", this, nameof(OnButtonPressed));
	}

	public void OnButtonPressed()
	{
		var tree = GetTree();
		tree.ChangeScene($"res://scene/{(messageType == MessageType.GameOver ? "Main" : "MainMenu")}.tscn");
		tree.Paused = false;
		Hide();
	}

	public void Show(MessageType type)
	{
		messageType = type;

		title.Text = messageType == MessageType.Victory
			? "Victory!"
			: "Game Over";

		message.Text = messageType == MessageType.Victory
			? "Level Completed"
			: "No More Presents";

		buttonLabel.Text = messageType == MessageType.Victory
			? "Title Screen"
			: "Try Again";

		Show();
	}
}