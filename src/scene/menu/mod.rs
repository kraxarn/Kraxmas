use macroquad::prelude::*;

mod menu;

pub struct Menu {
	// Windows
	settings_open: bool,
	// Settings
	audio_settings: crate::settings::AudioSettings,
	window_settings: crate::settings::WindowSettings,
}
