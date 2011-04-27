#pragma once

#include "SwapButtonsDll.h"

struct LayoutMetrics;

class LOCALMODE_API CThemeDwm
{
public:
	CThemeDwm(void);
	virtual ~CThemeDwm(void);

	bool IsAvailable();
	bool IsInUse(HWND hWndFrame);

	bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame);
	void PrepareFloatBar(HWND hWndFloatBar);

	void PaintStart(HDC hDC);
	void PaintBarBackground(HDC hDC, RECT rectBar);
	void PaintButtonFace(HDC hDC, RECT rectButton, HBITMAP hbmImage, HBITMAP hbmMask);
	void PaintButtonSpacing(HDC hDC, RECT rectButton);
	void PaintBarBorder(HDC hDC, RECT rectBar);
	void PaintEnd(HDC hDC);


private:
	void CheckIfAvailable();
	void SaveBgrColour();
	static COLORREF ARGBToColorref(DWORD ARGB);


private:
	bool m_bCheckedAvailable;
	bool m_bIsAvailable;
	HMODULE m_hDwmLib;

	COLORREF m_BgrColour;
};

