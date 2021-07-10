use macroquad::prelude::*;

impl super::Menu {
	pub fn main_menu(&mut self, ctx: &egui::CtxRef) {
		egui::Window::new("Main Menu")
			.default_pos(egui::Pos2::new(96_f32, screen_height() * 0.4_f32))
			.show(ctx, |ui| {
				ui.vertical_centered_justified(|ui| {
					if ui.button("Play").clicked() {
						todo!();
					}
					if ui.button("Create").clicked() {
						self.load_level.open = true;
					}
					if ui.button("Settings").clicked() {
						self.settings_open = !self.settings_open;
					}
					if ui.button("Quit").clicked() {
						std::process::exit(0);
					}
				});
			});
	}
}
