using Godot;

public class Hud : MarginContainer
{
	private int health;
	private int money;
	private int level;

	private Label healthLabel;
	private Label moneyLabel;
	private Label levelLabel;

	private Label debugInfo;

	public int Health
	{
		get => health;
		set
		{
			healthLabel.Text = value.ToString();
			health = value;
		}
	}

	public int Money
	{
		get => money;
		set
		{
			moneyLabel.Text = value.ToString();
			money = value;
		}
	}

	public int Level
	{
		get => level;
		set
		{
			if (value <= MaxLevel)
				levelLabel.Text = value.ToString();
			level = value;
		}
	}
	
#if DEBUG
	public const int MaxLevel = 2;
#else
	public const int MaxLevel = 10;
#endif

	public string DebugInfo
	{
		get => debugInfo.Text;
		set => debugInfo.Text = value;
	}

	public override void _Ready()
	{
		healthLabel = GetNode<Label>("HBox/Grid/Health");
		moneyLabel = GetNode<Label>("HBox/Grid/Money");
		levelLabel = GetNode<Label>("HBox/Grid/Level");
		debugInfo = GetNode<Label>("HBox/DebugInfo");

		Health = 100;
		Money = 100;
		Level = 1;
	}
}