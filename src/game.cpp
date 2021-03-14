#include "game.hpp"

game::game()
	: window("open-td-sdl", 1280, 720),
	loader("res"),
	font(loader.get_font("kenney_blocks.ttf", 46)),
	text(font)
{
	text.set_text("open-td");
	text.set_color(ce::color(0xf5, 0xf5, 0xf5));
	text.set_position(window.w() / 2 - (text.width() / 2), 100);
}

void game::run()
{
	while (window.is_open())
	{
		window.tick();

		window.clear();
		{
			window.draw(text);
		}
		window.render();
	}
}
