pub struct Game {
	pub current: Box<dyn super::scene::Scene>,
}

impl Default for Game {
	fn default() -> Self {
		Self {
			current: Box::new(super::scene::menu::Menu::new()),
		}
	}
}
