pub trait Scene {
	fn new() -> Self;
	fn update(&mut self);
}