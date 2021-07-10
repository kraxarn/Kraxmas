use macroquad::prelude::*;

mod load_level;
mod main_menu;
mod menu;
mod settings;

pub struct Menu {
	// Windows
	settings_open: bool,
	// Settings
	audio_settings: crate::settings::AudioSettings,
	window_settings: crate::settings::WindowSettings,
	// Load level list
	load_level: load_level::LoadLevel,
}
