pub fn label() -> macroquad::ui::Style {
	macroquad::ui::root_ui()
		.style_builder()
		.font(include_bytes!("../res/fonts/kenney_mini.ttf"))
		.unwrap()
		.text_color(crate::color::FOREGROUND)
		.build()
}

pub fn button(font_size: u16, padding: f32) -> macroquad::ui::Style {
	// TODO: This should be able to be calculated
	let button_margin = 19_f32;

	macroquad::ui::root_ui()
		.style_builder()
		.font(include_bytes!("../res/fonts/kenney_bold.ttf"))
		.unwrap()
		.font_size(font_size)
		.margin(macroquad::math::RectOffset::new(
			padding,
			0_f32,
			button_margin,
			0_f32,
		))
		.color(crate::color::BUTTON)
		.color_hovered(crate::color::HOVER)
		.color_clicked(crate::color::CLICK)
		.text_color(crate::color::FOREGROUND)
		.build()
}

pub fn window() -> macroquad::ui::Style {
	macroquad::ui::root_ui()
		.style_builder()
		.color(crate::color::MENU)
		.build()
}

pub fn group() -> macroquad::ui::Style {
	macroquad::ui::root_ui()
		.style_builder()
		.color(crate::color::BORDER)
		.build()
}
