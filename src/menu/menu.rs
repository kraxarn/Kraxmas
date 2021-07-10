use macroquad::prelude::*;

impl super::Menu {
	pub fn new() -> Self {
		Self {
			settings_open: false,

			audio_settings: super::audio_settings::AudioSettings::new(),
			window_settings: super::window_settings::WindowSettings::new(),
		}
	}
}

impl crate::scene::Scene for super::Menu {
	fn update(&mut self) {
		egui_macroquad::ui(|ctx| {
			egui::Window::new("Main Menu")
				.default_pos(egui::Pos2::new(96_f32, screen_height() * 0.4_f32))
				.show(ctx, |ui| {
					ui.vertical_centered_justified(|ui| {
						if ui.button("Play").clicked() {
							unimplemented!();
						}
						if ui.button("Create").clicked() {
							unimplemented!();
						}
						if ui.button("Settings").clicked() {
							self.settings_open = !self.settings_open;
						}
						if ui.button("Quit").clicked() {
							std::process::exit(0);
						}
					});
				});

			let audio_settings = &mut self.audio_settings;
			let window_settings = &mut self.window_settings;

			egui::Window::new("Settings")
				.open(&mut self.settings_open)
				.show(ctx, |ui| {
					ui.heading("Audio");
					egui::Grid::new("audio_grid").show(ui, |ui| {
						ui.label("Music");
						ui.add(egui::Slider::new(&mut audio_settings.music_volume, 0..=100));
						ui.end_row();

						ui.label("Sound");
						ui.add(egui::Slider::new(&mut audio_settings.sound_volume, 0..=100));
						ui.end_row();
					});

					ui.heading("Window");
					egui::Grid::new("window_grid").show(ui, |ui| {
						let resolution = super::window_settings::base_resolution()
							* window_settings.resolution_scale;
						ui.label("Resolution");
						egui::ComboBox::from_label("")
							.selected_text(format!("{}x{}", resolution.x, resolution.y))
							.show_ui(ui, |ui| {
								for scale in super::window_settings::all_scales() {
									let res = super::window_settings::base_resolution() * scale;
									ui.selectable_value(
										&mut window_settings.resolution_scale,
										1_f32,
										format!("{}x{}", res.x, res.y),
									);
								}
							});
						ui.end_row();
					});
				});
		});

		egui_macroquad::draw();
	}
}
