pub mod audio;
pub mod window;

pub struct AudioSettings {
	pub music_volume: f32,
	pub sound_volume: f32,
}

pub struct WindowSettings {
	pub fullscreen: bool,
	pub resolution_scale: f32,
	pub resolution: usize,
}
