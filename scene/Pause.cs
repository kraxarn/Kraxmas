using Godot;

public class Pause : Control
{
	private Main parent;

	private Slider music;
	private Slider sound;

	public override void _Ready()
	{
		parent = GetParent<Main>();

		GetNode<TextureButton>("Grid/Accept")
			.Connect("pressed", this, nameof(Accept));

		music = GetNode<Slider>("Grid/MusicSlider");
		sound = GetNode<Slider>("Grid/SoundSlider");
		
		var config = new Config();
		music.Value = config.MusicVolume;
		sound.Value = config.SoundVolume;

		music.Connect("value_changed", this, nameof(OnMusicSliderValueChanged));
	}

	public void Accept()
	{
		parent.Pause(false);

		var config = new Config
		{
			MusicVolume = (float) music.Value,
			SoundVolume = (float) sound.Value
		};
		config.Save();
	}

	public void OnMusicSliderValueChanged(float value)
	{
		parent.SetMusicVolume(value);
	}
}