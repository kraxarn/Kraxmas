use macroquad::prelude::*;

mod audio_settings;
mod window_settings;
mod menu;

pub struct Menu {
	// Windows
	settings_open: bool,
	// Settings
	audio_settings: audio_settings::AudioSettings,
	window_settings: window_settings::WindowSettings,
}
