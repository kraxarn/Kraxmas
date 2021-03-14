#include "ce/ui/messagebox.hpp"
#include "game.hpp"

void run_game()
{
	game g;
	g.run();
}

int main(int argc, char **argv)
{
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
