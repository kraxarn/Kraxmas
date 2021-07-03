use bevy::prelude::*;

mod menu;
mod game_state;

pub const APP_NAME: &str = "OpenTD: Alpha";

fn main() {
	App::build()
		.insert_resource(WindowDescriptor {
			title: APP_NAME.to_string(),
			width: 1280_f32,
			height: 720_f32,
			vsync: true,
			..Default::default()
		})
		.add_plugins(DefaultPlugins)
		.add_plugin(bevy_egui::EguiPlugin)
		.add_state(game_state::GameState::Menu)
		.add_system_set(SystemSet::on_enter(game_state::GameState::Menu)
			.with_system(menu::setup.system()))
		.add_system_set(SystemSet::on_update(game_state::GameState::Menu)
			.with_system(menu::update.system()))
		.run()
}
