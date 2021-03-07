#pragma once

#include <iostream>

#include <SDL.h>

/**
 * Application window
 */
class window
{
public:
	/**
	 * Create and show a new window
	 * @param title Window title
	 * @param x X position
	 * @param y Y position
	 * @param w Width
	 * @param h Height
	 */
	window(const std::string &title, int x, int y, int w, int h);

	/**
	 * Destroy window and free resources
	 */
	~window();

	/**
	 * Get SDL Renderer
	 * @note Shouldn't be needed
	 */
	SDL_Renderer *get_renderer();

	/**
	 * Clear window
	 * @param r Red
	 * @param g Green
	 * @param b Blue
	 * @return True if successful
	 */
	bool clear(Uint8 r = 0x00, Uint8 g = 0x00, Uint8 b = 0x00);

	/**
	 * Render to screen
	 */
	void present();

	/**
	 * Render texture to window
	 * @param texture Texture to render
	 * @param x X position
	 * @param y Y position
	 */
	void render(SDL_Texture *texture, int x, int y);

	/**
	 * Width
	 */
	int w();

	/**
	 * Height
	 */
	int h();

private:
	SDL_Window *sdl_window = nullptr;
	SDL_Renderer *sdl_renderer = nullptr;
};