use macroquad::prelude::*;
use macroquad::ui::hash;

impl super::Menu {
	pub fn main_menu(&mut self) {
		self.window_position = vec2(
			96_f32,
			(macroquad::window::screen_height() / 2_f32) - (self.window_size.y / 2_f32),
		);

		macroquad::ui::widgets::Window::new(hash!(), self.window_position, self.window_size)
			.label("Main Menu")
			.titlebar(false)
			.ui(&mut *macroquad::ui::root_ui(), |ui| {
				if self.button("Play", 0_f32).ui(ui) {
					todo!();
				}

				if self.button("Create", 1_f32).ui(ui) {
					self.load_level.open = !self.load_level.open;
					self.settings_open = false;
				}

				if self.button("Settings", 2_f32).ui(ui) {
					self.settings_open = !self.settings_open;
					self.load_level.open = false;
				}

				if self.button("Quit", 3_f32).ui(ui) {
					std::process::exit(0);
				}
			});
	}

	fn button(&self, label: &'static str, index: f32) -> macroquad::ui::widgets::Button {
		macroquad::ui::widgets::Button::new(label)
			.position(vec2(
				self.padding,
				(self.padding * (index + 1_f32)) + (self.buttons.size.y * index),
			))
			.size(self.buttons.size)
	}
}
