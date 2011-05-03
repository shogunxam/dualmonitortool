#pragma once

#include "SwapButtonsDll.h"
#include "Theme.h"

struct LayoutMetrics;

class LOCALMODE_API CThemeDwm : public CTheme
{
public:
	CThemeDwm(void);
	virtual ~CThemeDwm(void);

	virtual bool IsAvailable();
	virtual bool IsInUse(HWND hWndFrame);

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame);
	virtual void PrepareFloatBar(HWND hWndFloatBar);

	virtual void PaintStart(HDC hDC, RECT rectBar);
	virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	virtual void PaintButtonFace(HDC hDC, RECT rectButton, HBITMAP hbmImage, HBITMAP hbmMask);
	virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	virtual void PaintEnd(HDC hDC);


private:
	void CheckIfAvailable();
	void SaveBgrColour();

private:
	static const int LEFT_BORDER;
	static const int RIGHT_BORDER;
	static const int TOP_BORDER;
	static const int BOTTOM_BORDER;
	static const int BUTTON_SPACING;

private:
	bool m_bCheckedAvailable;
	bool m_bIsAvailable;
	HMODULE m_hDwmLib;

	COLORREF m_BgrColour;
};

