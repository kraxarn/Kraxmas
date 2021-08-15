use macroquad::prelude::*;
use macroquad::ui::hash;

impl super::Menu {
	pub fn settings(&mut self) {
		if !self.settings_open {
			return;
		}

		let window_position = vec2(
			self.window_position.x + self.window_size.x + (self.padding * 2_f32),
			self.window_position.y,
		);

		macroquad::ui::widgets::Window::new(hash!(), window_position, self.window_size)
			.label("Settings")
			.titlebar(false)
			.ui(&mut *macroquad::ui::root_ui(), |ui| {
				ui.label(vec2(self.padding, self.padding), "Audio");
				macroquad::ui::widgets::Group::new(
					hash!(),
					vec2(self.window_size.x - self.padding * 2_f32, 48_f32),
				)
				.position(vec2(self.padding, 32_f32))
				.ui(ui, |ui| {
					ui.drag(
						hash!(),
						"Music",
						Some((0_f32, 100_f32)),
						&mut self.audio_settings.music_volume,
					);

					ui.drag(
						hash!(),
						"Sound",
						Some((0_f32, 100_f32)),
						&mut self.audio_settings.sound_volume,
					);
				});
			});
	}
}
