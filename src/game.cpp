#include "game.hpp"

#include "ce/engine.hpp"
#include "ce/format.hpp"

game::game()
	: window(ce::fmt::format("OpenTD - {}", ce::engine::version()), 1280, 720),
	loader("res"),
	font(loader.get_font("kenney_blocks.ttf", 46)),
	text(font)
{
	text.set_text("open-td");
	text.set_color(ce::color(0xf5, 0xf5, 0xf5));
	text.set_position(window.width() / 2 - (text.width() / 2), 100);
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
