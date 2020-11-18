using Godot;
using System;

public class Hud : MarginContainer
{
	private int health;
	private int money;

	private Label healthLabel;
	private Label moneyLabel;

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

	public string DebugInfo
	{
		get => debugInfo.Text;
		set => debugInfo.Text = value;
	}

	public override void _Ready()
	{
		healthLabel = GetNode<Label>("HBox/Grid/Health");
		moneyLabel = GetNode<Label>("HBox/Grid/Money");
		debugInfo = GetNode<Label>("HBox/DebugInfo");

		Health = 100;
		Money = 500;
	}
}