use macroquad::prelude::*;

mod color;
mod game;
mod scene;
pub mod settings;
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
	let mut game: game::Game = Default::default();

	set_panic_handler(|msg, backtrace| async move {
		loop {
			clear_background(color::BACKGROUND);
			let x = 16_f32;
			let mut y = 72_f32;
			let font_size = 24_f32;

			draw_text("Fatal Error :(", x, 24_f32, font_size, color::ERROR);
			draw_text(&msg, x, 44_f32, font_size, color::FOREGROUND);

			for line in backtrace.split('\n') {
				draw_text(line, x, y, font_size, color::FOREGROUND);
				y += 22_f32;
			}

			next_frame().await;
		}
	});

	loop {
		clear_background(color::BACKGROUND);

		game.current.update();

		draw_text(
			&format!("FPS: {}", get_fps()),
			16_f32,
			24_f32,
			22_f32,
			color::FOREGROUND,
		);

		next_frame().await
	}
}
