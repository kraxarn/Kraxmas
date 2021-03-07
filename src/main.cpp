#include <iostream>

#include <SDL.h>
#include <SDL_ttf.h>

#include "window.hpp"
#include "label.hpp"

int main()
{
	if (SDL_Init(SDL_INIT_VIDEO) != 0)
	{
		std::cerr << "Init: " << SDL_GetError() << std::endl;
		return 1;
	}

	if (TTF_Init() != 0)
	{
		std::cerr << "TTF init: " << SDL_GetError() << std::endl;
		return 1;
	}

	// This needs to be scoped to free resources before quitting the application
	{
		window w("open-td (SDL)", 100, 100, 1280, 720);

//		auto bmp = SDL_LoadBMP("kraxie.bmp");
//		if (bmp == nullptr)
//		{
//			std::cerr << "LoadBMP: " << SDL_GetError() << std::endl;
//			SDL_Quit();
//			return 1;
//		}
//
//		auto texture = SDL_CreateTextureFromSurface(w.get_renderer(), bmp);
//		SDL_FreeSurface(bmp);
//		if (texture == nullptr)
//		{
//			std::cerr << "CreateTextureFromSurface: " << SDL_GetError() << std::endl;
//			SDL_Quit();
//			return 1;
//		}

		label text("res/fonts/kenney_blocks.ttf", 46);
		text.set_text("open-td");
		text.set_color(0xf5, 0xf5, 0xf5);

		auto running = true;
		SDL_Event event;

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

			w.clear();

			text.render(w, w.w() / 2 - (text.w() / 2), 100);

			//w.render(texture, 10, 10);
			//SDL_RenderCopy(w.get_renderer(), texture, nullptr, nullptr);
			w.present();
		}

		//SDL_DestroyTexture(texture);
	}

	TTF_Quit();
	SDL_Quit();
	return 0;
}
