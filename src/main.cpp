#include "ce/engine.hpp"
#include "ce/log.hpp"
#include "ce/ui/messagebox.hpp"

#include "game.hpp"

void run_game()
{
	game g;
	g.run();
}

int main(int argc, char **argv)
{
	ce::log::info(ce::engine::get_version());

#ifdef NDEBUG
	try
	{
		run_game();
	}
	catch (const std::exception &e)
	{
		ce::message_box::error("Error", e.what());
	}
#else
	run_game();
#endif

	return 0;
}
