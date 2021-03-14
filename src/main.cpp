#include "ce/engine.hpp"
#include "ce/log.hpp"
#include "ce/ui/messagebox.hpp"

#include "game.hpp"

int main(int argc, char **argv)
{
	ce::log::info(ce::engine::get_version());

	// This might be a bad idea
	try
	{
		game g;
		g.run();
	}
	catch (const std::exception &e)
	{
		ce::message_box::error("Error", e.what());
	}

	return 0;
}
