use bevy::prelude::*;
use bevy_egui::egui;
use bevy::math::vec2;

pub fn setup(mut commands: Commands, asset_server: Res<AssetServer>) {}

fn all_resolutions() -> Vec<Vec2> {
	vec![0.75_f32, 1_f32, 1.5_f32, 2_f32].into_iter().map(|r| {
		vec2(1280_f32 * r, 720_f32 * r)
	}).collect()
}

pub fn update(ui: Res<bevy_egui::EguiContext>) {
	egui::Window::new("Main Menu").show(ui.ctx(), |ui| {
		ui.vertical_centered_justified(|ui| {
			ui.button("Play");
			ui.button("Create");
			ui.button("Settings");
			ui.button("Quit");
		});
	});

	// Audio
	let mut music_volume = 100_u8;
	let mut sound_volume = 100_u8;
	// Window
	let mut fullscreen = false;
	let mut resolution = all_resolutions()[1];

	egui::Window::new("Settings").show(ui.ctx(), |ui| {
		ui.heading("Audio");
		egui::Grid::new("audio_grid").show(ui, |ui| {
			ui.label("Music");
			ui.add(egui::Slider::new(&mut music_volume, 0..=100));
			ui.end_row();

			ui.label("Sound");
			ui.add(egui::Slider::new(&mut sound_volume, 0..=100));
			ui.end_row();
		});

		ui.heading("Window");
		egui::Grid::new("window_grid").show(ui, |ui| {
			ui.label("Resolution");
			egui::ComboBox::from_label("")
				.selected_text(format!("{}x{}", resolution.x, resolution.y))
				.show_ui(ui, |ui| {
					for res in all_resolutions() {
						ui.selectable_value(&mut resolution,
											all_resolutions()[1],
											format!("{}x{}", res.x, res.y));
					}
				});
			ui.end_row();
		});
	});
}

pub fn exit(mut commands: Commands) {}