use macroquad::prelude::*;

impl super::Menu {
	pub fn new() -> Self {
		let mut menu = Self {
			settings_open: false,
			audio_settings: Default::default(),
			load_level: Default::default(),
			skin: macroquad::ui::root_ui().default_skin(),
			window_size: vec2(250_f32, 300_f32),
			window_position: Vec2::ZERO,
			padding: 12_f32,
			buttons: Default::default(),
		};

		menu.buttons = menu.get_button_data();
		menu.skin = menu.get_skin();
		menu.load_level.size = menu.window_size;

		menu
	}

	fn get_button_data(&self) -> super::ButtonData {
		super::ButtonData {
			size: self.get_button_size(),
		}
	}

	fn get_button_size(&self) -> glam::Vec2 {
		let button_count = 4_f32;
		let button_height =
			(self.window_size.y - (self.padding * (button_count + 1_f32))) / button_count;
		vec2(self.window_size.x - (self.padding * 2_f32), button_height)
	}

	fn get_skin(&self) -> macroquad::ui::Skin {
		macroquad::ui::Skin {
			label_style: crate::style::label(),
			button_style: crate::style::button(22_u16, self.padding),
			// tabbar_style
			window_style: crate::style::window(),
			// editbox_style
			// window_titlebar_style
			// scrollbar_style
			// scrollbar_handle_style
			// checkbox_style
			group_style: crate::style::group(),
			..macroquad::ui::root_ui().default_skin()
		}
	}
}

impl crate::scene::Scene for super::Menu {
	fn update(&mut self) {
		macroquad::ui::root_ui().push_skin(&self.skin);

		self.main_menu();
		self.settings();

		self.load_level
			.set_position(self.window_position, self.window_size);
		self.load_level.push_skin(&self.skin);
		self.load_level.update();
	}
}
