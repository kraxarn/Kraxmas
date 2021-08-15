use macroquad::prelude::*;

impl Default for super::WindowSettings {
	fn default() -> Self {
		Self {
			fullscreen: false,
			resolution_scale: 1_f32,
			resolution: 1,
		}
	}
}

pub const RESOLUTION_COUNT: usize = 4;

pub fn all_scales() -> Vec<f32> {
	vec![0.75_f32, 1_f32, 1.5_f32, 2_f32]
}

pub fn base_resolution() -> Vec2 {
	vec2(1280_f32, 720_f32)
}

pub fn resolutions() -> [&'static str; RESOLUTION_COUNT] {
	[
		"960x540",   // 0.75x
		"1280x720",  // 1x
		"1920x1080", // 1.5x
		"2560x1440", // 2x
	]
}
