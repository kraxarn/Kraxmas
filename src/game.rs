pub struct Game {
	pub current: Box<dyn crate::scene::Scene>,
}

impl Default for Game {
	fn default() -> Self {
		Self {
			current: Box::new(crate::scene::menu::Menu::new()),
		}
	}
}
