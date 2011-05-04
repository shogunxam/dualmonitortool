#pragma once

#include "SwapButtonsDll.h"
#include "Theme.h"
#include "BitmapBuffer.h"

struct LayoutMetrics;

class LOCALMODE_API CThemeClassic : public CTheme
{
public:
	CThemeClassic(void);
	virtual ~CThemeClassic(void);

	virtual void LoadBitmaps(HMODULE hModule);
	virtual bool IsAvailable();
	virtual bool IsInUse(HWND hWndFrame);

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame);
	virtual SIZE CalcBarOffsets(HWND hWndFrame);
	virtual void PrepareFloatBar(HWND hWndFloatBar);

	virtual void PaintStart(HDC hDC, RECT rectBar);
	virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	virtual void PaintButtonFace(HDC hDC, RECT rectButton, int index);
	virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	virtual void PaintEnd(HDC hDC);


private:
	CBitmapBuffer m_BitmapBuffer;


private:
	static const int LEFT_BORDER;
	static const int RIGHT_BORDER;
	static const int TOP_BORDER;
	static const int BOTTOM_BORDER;
	static const int BUTTON_SPACING;

};
