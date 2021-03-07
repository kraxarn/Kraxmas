#include "label.hpp"

label::label(const std::string &file, int size)
{
	auto fs = cmrc::otd::res::get_filesystem();

	if (!fs.is_file(file))
	{
		std::cerr << "No such font: " << file << std::endl;
		return;
	}

	auto data = fs.open(file);

	auto src = SDL_RWFromMem((void *) data.begin(), data.size());
	if (src == nullptr)
	{
		std::cerr << "SDL_RWFromMem: " << SDL_GetError() << std::endl;
		return;
	}

	font = TTF_OpenFontRW(src, true, size);
	if (font == nullptr)
	{
		std::cerr << "TTF_OpenFontRW: " << TTF_GetError() << std::endl;
		return;
	}
}

label::~label()
{
	if (text_surface != nullptr)
		SDL_FreeSurface(text_surface);

	if (texture != nullptr)
		SDL_DestroyTexture(texture);

	if (font != nullptr)
		TTF_CloseFont(font);
}

void label::set_text(const std::string &text)
{
	render_text = text;
	update_text_surface();
}

void label::set_color(Uint8 r, Uint8 g, Uint8 b)
{
	color = {
		r, g, b,
	};
	update_text_surface();
}

void label::render(window &w, int x, int y)
{
	if (texture == nullptr)
		texture = SDL_CreateTextureFromSurface(w.get_renderer(), text_surface);

	w.render(texture, x, y);
}

int label::w()
{
	return text_surface->w;
}

int label::h()
{
	return text_surface->h;
}

void label::update_text_surface()
{
	if (text_surface != nullptr)
		SDL_FreeSurface(text_surface);
	text_surface = TTF_RenderText_Solid(font, render_text.c_str(), color);

	if (texture != nullptr)
		SDL_DestroyTexture(texture);
	texture = nullptr;
}
