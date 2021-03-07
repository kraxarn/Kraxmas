#include "game.hpp"

game::game()
	: w("open-td-sdl", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 1280, 720),
	text("res/fonts/kenney_blocks.ttf", 46),
	event(SDL_Event{})
{
	text.set_text("open-td");
	text.set_color(0xf5, 0xf5, 0xf5);
}

void game::run()
{
	while (running)
	{
		while (SDL_PollEvent(&event))
		{
			if (event.type == SDL_QUIT)
			{
				running = false;
				break;
			}
		}

		w.clear(0x21, 0x21, 0x21);
		{
			text.render(w, w.w() / 2 - (text.w() / 2), 100);
		}
		w.present();
	}
}
