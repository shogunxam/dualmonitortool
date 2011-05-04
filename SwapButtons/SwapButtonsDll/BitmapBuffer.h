#pragma once

#include "SwapButtonsDll.h"

// The idea of this class was to build the bar buttons in here with alpha channel values
// but not for the alpha values to have an impact while being drawn into here,
// but only when they get drawn into the window's DC.
//
// but seeing as I can't get transparency working anyway, It may be simpler (and faster) 
// to create a DC and draw directly into that, but we'll see.
//
class LOCALMODE_API CBitmapBuffer
{
public:
	CBitmapBuffer();
	CBitmapBuffer(int nWidth, int nHeight);
	~CBitmapBuffer(void);

	void Init(int nWidth, int nHeight);

	int GetWidth() const;
	int GetHeight() const;
	const DWORD* GetBits() const;

	void Fill(int x1, int y1, int x2, int y2, DWORD dwColour);
	void DrawHLine(int x1, int x2, int y, DWORD dwColour);
	void DrawVLine(int x, int y1, int y2, DWORD dwColour);
	void Set(int x, int y, DWORD dwColour);
	void CenterTransparentBitmap(HBITMAP hbm, int left, int top, int right, int bottom, DWORD dwTransparent);


private:
	void InlineSet(int x, int y, DWORD dwColour);
	bool IsValidX(int x);
	bool IsValidY(int y);

private:
	int m_nWidth;
	int m_nHeight;
	DWORD* m_pdwBits;
};

