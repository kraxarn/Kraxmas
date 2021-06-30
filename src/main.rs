use macroquad::prelude::*;

fn window_conf() -> Conf {
	Conf {
		window_title: "OpenTD: Alpha".to_owned(),
		window_width: 1280_i32,
		window_height: 720_i32,
		..Default::default()
	}
}

#[macroquad::main(window_conf)]
async fn main() {
	let background_color = Color::from_rgba(0x21, 0x21, 0x21, 0xff);

	loop {
		clear_background(background_color);

		draw_text("OpenTD: Alpha", 32_f32, 32_f32, 28_f32, WHITE);

		next_frame().await
	}
}
