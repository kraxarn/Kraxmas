#pragma once

#include <iostream>

#include <SDL.h>
#include <SDL_ttf.h>

#include "res.hpp"
#include "window.hpp"

/**
 * Text label
 */
class label
{
public:
	/**
	 * Create a new label
	 * @param file Path to resource file
	 * @param size
	 */
	label(const std::string &file, int size);

	/**
	 * Delete label and free resources
	 */
	~label();

	/**
	 * Set text
	 * @note Re-renders text
	 */
	void set_text(const std::string &text);

	/**
	 * Set color
	 * @note Re-renders text
	 */
	void set_color(Uint8 r, Uint8 g, Uint8 b);

	/**
	 * Render text to window
	 * @param w Window to render to
	 * @param x X position
	 * @param y Y position
	 * @note Creates a new texture for the first frame
	 */
	void render(window &w, int x, int y);

	/**
	 * Width
	 */
	int w();

	/**
	 * Height
	 */
	int h();

private:
	TTF_Font *font = nullptr;
	std::string render_text;
	SDL_Color color = {0, 0, 0};
	SDL_Surface *text_surface = nullptr;
	SDL_Texture *texture = nullptr;

	void update_text_surface();
};