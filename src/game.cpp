#include "game.hpp"

game::game()
	: w("open-td-sdl", 0, 0, 1280, 720),
	text("res/fonts/kenney_blocks.ttf", 46)
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

			w.clear();

			text.render(w, w.w() / 2 - (text.w() / 2), 100);

			w.present();
		}
	}
}
