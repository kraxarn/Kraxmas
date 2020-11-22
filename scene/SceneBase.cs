using Godot;

public abstract class SceneBase : Node
{
	[Export] public PackedScene Enemy;

	protected Hud Hud { get; set; }

	protected Timer EnemyTimer;
	
	protected AudioStreamPlayer Music;

	protected virtual int TotalEnemies { get; set; }

	public abstract void Pause(bool paused);

	public virtual void OnEnemyTimerTimeout()
	{
		var enemy = (Enemy) Enemy.Instance();
		enemy.Health = Hud?.Level ?? 1;
		enemy.AddToGroup("enemies");
		AddChild(enemy);
		TotalEnemies++;
	}
	
	public void SetMusicVolume(float value)
	{
		Music.VolumeDb = Mathf.Log(value) * 20;
	}
}