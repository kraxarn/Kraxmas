impl super::Menu {
	pub fn new() -> Self {
		Self {
			settings_open: false,
			audio_settings: Default::default(),
			window_settings: Default::default(),
			load_level: Default::default(),
		}
	}
}

impl crate::scene::Scene for super::Menu {
	fn update(&mut self) {
		egui_macroquad::ui(|ctx| {
			self.main_menu(ctx);
			self.settings(ctx);
			self.load_level.update(ctx);
		});
		egui_macroquad::draw();
	}
}
