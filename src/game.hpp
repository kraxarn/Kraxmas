#pragma once

#include "ce/window.hpp"
#include "ce/graphics/text.hpp"
#include "ce/font.hpp"
#include "ce/assets/fileloader.hpp"

class game
{
public:
	game();

	void run();

private:
	ce::window window;
	ce::file_loader loader;
	ce::font font;
	ce::text text;
	bool running = true;
};