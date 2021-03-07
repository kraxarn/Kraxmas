#include <iostream>

#include <SDL.h>
#include <SDL_ttf.h>

#include "game.hpp"

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

	{
		game g;
		g.run();
	}

	TTF_Quit();
	SDL_Quit();
	return 0;
}
