use macroquad::prelude::*;

mod button_data;
mod load_level;
mod main_menu;
mod menu;
mod settings;

pub struct ButtonData {
	size: Vec2,
}

pub struct Menu {
	// Windows
	settings_open: bool,
	// Settings
	audio_settings: crate::settings::AudioSettings,
	// Load level list
	load_level: load_level::LoadLevel,

	/// UI skin
	skin: macroquad::ui::Skin,
	/// Size of main menu window
	window_size: Vec2,
	/// Position of main menu window
	window_position: Vec2,
	/// Padding for all elements
	padding: f32,
	/// Various data and styles related to buttons
	buttons: ButtonData,
}
