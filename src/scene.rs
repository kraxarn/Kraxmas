use std::future::Future;

pub trait Scene {
	fn new() -> Self;
	fn update(&mut self);
}