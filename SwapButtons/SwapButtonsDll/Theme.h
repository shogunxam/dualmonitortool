#pragma once

#include "SwapButtonsDll.h"

class LOCALMODE_API CTheme
{
public:
	CTheme(void);
	virtual ~CTheme(void);

	virtual bool IsAvailable() = 0;
	virtual bool IsInUse(HWND hWndFrame) = 0;

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame) = 0;
	virtual void PrepareFloatBar(HWND hWndFloatBar);

	virtual void PaintStart(HDC hDC);
	virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	virtual void PaintButtonFace(HDC hDC, RECT rectButton, HBITMAP hbmImage, HBITMAP hbmMask) = 0;
	virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	virtual void PaintEnd(HDC hDC);

protected:
		static COLORREF ARGBToColorref(DWORD ARGB);

};

