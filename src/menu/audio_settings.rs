pub struct AudioSettings {
	pub music_volume: u8,
	pub sound_volume: u8,
}

impl AudioSettings {
	pub fn new() -> Self {
		Self {
			music_volume: 100,
			sound_volume: 100,
		}
	}
}
