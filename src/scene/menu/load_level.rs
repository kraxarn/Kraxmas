use macroquad::math::{vec2, Vec2};
use macroquad::ui::hash;

pub struct LoadLevel {
	pub open: bool,
	files: Vec<String>,
	selected: Option<usize>,
	pub size: Vec2,
	pub position: Vec2,
}

impl Default for LoadLevel {
	fn default() -> Self {
		Self {
			open: false,
			files: (1..=20)
				.map(|i| format!("Level {}", i).to_owned())
				.collect(),
			selected: None,
			size: Vec2::ZERO,
			position: Vec2::ZERO,
		}
	}
}

impl LoadLevel {
	pub fn set_position(&mut self, position: Vec2, size: Vec2) {
		self.position = vec2(position.x + size.x + (24_f32), position.y);
	}

	pub fn push_skin(&mut self, base: &macroquad::ui::Skin) {
		let mut skin = base.clone();
		skin.button_style = crate::style::button(12_u16, 2_f32);
		macroquad::ui::root_ui().push_skin(&skin);
	}

	pub fn update(&mut self) {
		if !self.open {
			return;
		}

		let padding = 12_f32; // TODO
		let padding_vec = vec2(padding, padding);

		macroquad::ui::widgets::Window::new(hash!(), self.position, self.size)
			.label("Levels")
			.titlebar(false)
			.ui(&mut *macroquad::ui::root_ui(), |ui| {
				macroquad::ui::widgets::Group::new(
					hash!(),
					vec2(self.size.x - (padding * 2_f32), self.size.y - 64_f32),
				)
				.position(padding_vec)
				.ui(ui, |ui| {
					for (i, file) in self.files.iter().enumerate() {
						let mut is_selected = match self.selected {
							Some(s) => s == i,
							None => false,
						};
						ui.checkbox(hash!(), file, &mut is_selected);
						if is_selected {
							self.selected = Some(i);
						}
					}
				});

				if ui.button(vec2(padding, self.size.y - 42_f32), "New") {
					todo!();
				}

				if ui.button(vec2(padding + 64_f32, self.size.y - 42_f32), "Load") {
					todo!();
				}
			});
	}
}
