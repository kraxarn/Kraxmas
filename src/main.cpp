#include "ce/engine.hpp"
#include "ce/log.hpp"

#include "game.hpp"

int main(int argc, char **argv)
{
	ce::engine engine;
	ce::log::info(ce::engine::get_version());

	{
		game g;
		g.run();
	}

	return 0;
}
