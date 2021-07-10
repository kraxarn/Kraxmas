pub mod audio;
pub mod window;

pub struct AudioSettings {
	pub music_volume: u8,
	pub sound_volume: u8,
}

pub struct WindowSettings {
	pub fullscreen: bool,
	pub resolution_scale: f32,
	pub resolution: u8,
}
