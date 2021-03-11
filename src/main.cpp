#include <iostream>
#include <sstream>

#include <SDL.h>
#include <SDL_ttf.h>

#include "game.hpp"

std::string sdl_linked_version()
{
	SDL_version version;
	SDL_GetVersion(&version);

	std::stringstream ss;
	ss << std::to_string(version.major) << "."
		<< std::to_string(version.minor) << "."
		<< std::to_string(version.patch);
	return ss.str();
}

int main(int argc, char **argv)
{
	if (SDL_Init(SDL_INIT_VIDEO) != 0)
	{
		std::cerr << "Init: " << SDL_GetError() << std::endl;
		return 1;
	}
	std::cout << "SDL " << sdl_linked_version() << std::endl;

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
