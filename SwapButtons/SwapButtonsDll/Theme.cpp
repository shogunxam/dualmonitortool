#include "StdAfx.h"
#include "Theme.h"


CTheme::CTheme(void)
{
}


CTheme::~CTheme(void)
{
}

// virtual 
void CTheme::PrepareFloatBar(HWND hWndFloatBar)
{
}

// virtual 
void CTheme::PaintStart(HDC hDC)
{
	// base implementation does nothing
}

// virtual 
void CTheme::PaintBarBackground(HDC hDC, RECT rectBar)
{
	// base implementation does nothing
}

// virtual 
void CTheme::PaintButtonSpacing(HDC hDC, RECT rectButton)
{
	// base implementation does nothing
}

// virtual 
void CTheme::PaintBarBorder(HDC hDC, RECT rectBar)
{
	// base implementation does nothing
}

// virtual 
void CTheme::PaintEnd(HDC hDC)
{
	// base implementation does nothing
}

///////////////////////// static helper functions /////////////////////////

// private static
COLORREF CTheme::ARGBToColorref(DWORD ARGB)
{
	DWORD red =   (ARGB & 0x00FF0000) >> 16;
	DWORD green = (ARGB & 0x0000ff00) >> 8;
	DWORD blue =  (ARGB & 0x000000FF);
	DWORD alpha = (ARGB & 0xFF000000) >> 24;

	// assume the colour underneath is going to be white
	DWORD blend = 0xFF;	// for red, green and blue
	// and adust the colours according to the alpha

#define BLEND_COLOUR(in, alpha, blend) ((in * alpha + blend * (0xFF - alpha)) / 0xFF)
	red = BLEND_COLOUR(red, alpha, blend);
	green = BLEND_COLOUR(green, alpha, blend);
	blue = BLEND_COLOUR(blue, alpha, blend);
#undef BLEND_COLOUR

	return RGB(red, green, blue);
}
