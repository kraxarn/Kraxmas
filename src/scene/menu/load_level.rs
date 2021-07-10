pub struct LoadLevel {
	pub open: bool,
	files: Vec<String>,
	selected: Option<usize>,
}

impl Default for LoadLevel {
	fn default() -> Self {
		Self {
			open: false,
			files: (1..10).map(|i| format!("File {}", i).to_owned()).collect(),
			selected: None,
		}
	}
}

impl LoadLevel {
	pub fn update(&mut self, ctx: &egui::CtxRef) {
		let open = &mut self.open;
		let files = &mut self.files;
		let selected = &mut self.selected;

		egui::Window::new("Levels").open(open).show(ctx, |ui| {
			let row_height = ui.fonts()[egui::TextStyle::Body].row_height();
			ui.vertical(|ui| {
				egui::ScrollArea::auto_sized().show_rows(
					ui,
					row_height,
					files.len(),
					|ui, row_range| {
						for i in row_range {
							let checked = match selected {
								Some(c) => *c == i,
								None => String::,
							};
							if ui.selectable_label(checked, &files[i]).clicked() {
								*selected = Some(i);
							};

							ui.selectable_value(mat)
						}
					},
				);
			});

			ui.separator();

			ui.horizontal(|ui| {
				if ui.button("Load").clicked() {
					todo!();
				}
				if ui.button("New").clicked() {
					todo!();
				}
			});
		});
	}
}
