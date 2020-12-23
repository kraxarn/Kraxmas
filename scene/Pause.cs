using Godot;

public class Pause : Control
{
	private SceneBase parent;

	private Slider music;
	private Slider sound;

	private TextureButton accept;

	public bool AcceptVisible
	{
		set => accept.Visible = value;
	}

	public Color TextColor
	{
		set
		{
			GetNode<Label>("Grid/MusicLabel").Modulate = value;
			GetNode<Label>("Grid/SoundLabel").Modulate = value;
		}
	}

	public override void _Ready()
	{
		parent = GetParent<SceneBase>();

		accept = GetNode<TextureButton>("Grid/Accept");
		accept.Connect("pressed", this, nameof(Accept));

		music = GetNode<Slider>("Grid/MusicSlider");
		sound = GetNode<Slider>("Grid/SoundSlider");

		var config = new Config();
		music.Value = config.MusicVolume;
		sound.Value = config.SoundVolume;

		music.Connect("value_changed", this, nameof(OnMusicSliderValueChanged));
		sound.Connect("value_changed", this, nameof(OnSoundSliderValueChanged));
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

	public void OnSoundSliderValueChanged(float value)
	{
		parent.SetSoundVolume(value);
	}
}