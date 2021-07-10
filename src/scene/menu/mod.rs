use macroquad::prelude::*;

mod audio_settings;
mod menu;
mod window_settings;

pub struct Menu {
	// Windows
	settings_open: bool,
	// Settings
	audio_settings: audio_settings::AudioSettings,
	window_settings: window_settings::WindowSettings,
}
