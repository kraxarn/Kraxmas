#pragma once

#include "window.hpp"
#include "label.hpp"

class game
{
public:
	game();

	void run();

private:
	window w;
	label text;
	bool running = true;
	SDL_Event  event;
};