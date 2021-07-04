use macroquad::prelude::*;

mod menu;
mod style;

pub const APP_NAME: &str = "OpenTD: Alpha";

fn window_conf() -> Conf {
	Conf {
		window_title: APP_NAME.to_owned(),
		window_width: 1280_i32,
		window_height: 720_i32,
		..Default::default()
	}
}

#[macroquad::main(window_conf)]
async fn main() {
	let background_color = Color::from_rgba(0x21, 0x21, 0x21, 0xff);
	let mut menu = menu::Menu::new();

	loop {
		clear_background(background_color);

		menu.update().await;

		next_frame().await
	}
}
