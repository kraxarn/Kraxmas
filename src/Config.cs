using Godot;

public class Config
{
	private const string Path = "user://settings.cfg";

	private readonly ConfigFile configFile;

	public float MusicVolume
	{
		get => configFile.GetValue("audio", "music") as float? ?? 0.8f;
		set => configFile.SetValue("audio", "music", value);
	}

	public float SoundVolume
	{
		get => configFile.GetValue("audio", "sound") as float? ?? 0.8f;
		set => configFile.SetValue("audio", "sound", value);
	}

	public Config()
	{
		configFile = new ConfigFile();
		
		var error = configFile.Load(Path);
		if (error != Error.Ok)
		{
			GD.PrintErr("Failed to load settings:", error);
		}
	}

	public void Save()
	{
		var error = configFile.Save(Path);
		if (error != Error.Ok)
		{
			GD.PrintErr("Failed to save settings:", error);
		}
	}
}