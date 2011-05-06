#pragma once

#include "SwapButtonsDll.h"

#include <UxTheme.h>
#include <vsstyle.h>
#include <vssym32.h>

#include "Theme.h"
//#include "BitmapBuffer.h"

struct LayoutMetrics;

class LOCALMODE_API CThemeBasic : public CTheme
{
public:
	CThemeBasic(void);
	virtual ~CThemeBasic(void);

	virtual void LoadBitmaps(HMODULE hModule);
	virtual bool IsAvailable();
	virtual bool IsInUse(HWND hWndFrame);

	virtual bool ReInit(struct LayoutMetrics* pLayoutMetrics, HWND hWndFrame);
	virtual SIZE CalcBarOffsets(HWND hWndFrame);
	virtual void PrepareFloatBar(HWND hWndFloatBar);
	virtual void PaintBar(HWND hWndFloatBar, HDC hDC, const CButtonList& buttonList, RECT rectBar);

	//virtual void PaintStart(HDC hDC, RECT rectBar);
	//virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	//virtual void PaintButtonFace(HDC hDC, RECT rectButton, int index);
	//virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	//virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	//virtual void PaintEnd(HDC hDC, RECT rectBar);

private:
	void CheckIfAvailable();
	void SaveBgrColour();

	void PaintStart(HDC hDC, RECT rectBar);
	void PaintBarBackground(HDC hDC, RECT rectBar);
	void PaintButtonFace(HWND hWndFloatBar, HDC hDC, RECT rectButton, int index);
	void PaintButtonSpacing(HDC hDC, RECT rectButton);
	void PaintBarBorder(HDC hDC, RECT rectBar);
	void PaintEnd(HDC hDC, RECT rectBar);

private:
	HTHEME m_hTheme;
	HDC m_hDCMem;
	HBITMAP m_hbmBar;
	HBITMAP m_hbmOld;

//	CBitmapBuffer m_BitmapBuffer;
	HBITMAP m_hbmBackground;

	bool m_bCheckedAvailable;
	bool m_bIsAvailable;
	HMODULE m_hDwmLib;

	COLORREF m_BgrColour;

	int m_nButtonWidth;
	int m_nButtonHeight;


private:
	static const int LEFT_BORDER;
	static const int RIGHT_BORDER;
	static const int TOP_BORDER;
	static const int BOTTOM_BORDER;
	static const int BUTTON_SPACING;
};