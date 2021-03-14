#include "game.hpp"

game::game()
	: window("open-td-sdl", 1280, 720),
	loader("res"),
	font(loader.get_font("kenney_blocks.ttf", 46)),
	text(font)
{
	text.set_text("open-td");
	text.set_color(ce::color(0xf5, 0xf5, 0xf5));
}

void game::run()
{
	while (window.get_running())
	{
		window.tick();

		window.clear();
		{
			window.render(text, window.w() / 2 - (text.width() / 2), 100);
		}
		window.present();
	}
}
