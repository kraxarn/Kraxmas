use macroquad::prelude::*;

mod main_menu;
mod menu;
mod settings;

pub struct Menu {
	// Windows
	settings_open: bool,
	// Settings
	audio_settings: crate::settings::AudioSettings,
	window_settings: crate::settings::WindowSettings,
}
