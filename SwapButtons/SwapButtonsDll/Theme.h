#pragma once

#include "SwapButtonsDll.h"

class LOCALMODE_API CTheme
{
public:
	CTheme(void);
	virtual ~CTheme(void);

	virtual void LoadBitmaps(HMODULE hModule) = 0;
	virtual HBITMAP GetImage(int index);

	virtual bool IsAvailable() = 0;
	virtual bool IsInUse(HWND hWndFrame) = 0;

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame) = 0;
	virtual SIZE CalcBarOffsets(HWND hWndFrame) = 0;
	virtual void PrepareFloatBar(HWND hWndFloatBar);

	virtual void PaintStart(HDC hDC, RECT rectBar);
	virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	virtual void PaintButtonFace(HDC hDC, RECT rectButton, int index) = 0;
	virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	virtual void PaintEnd(HDC hDC);

protected:
		static COLORREF ARGBToColorref(DWORD ARGB);
		static DWORD ARGBBlend(DWORD ARGB);

protected:
	HBITMAP m_hbmPrev;
	HBITMAP m_hbmNext;
	HBITMAP m_hbmSupersize;

};
