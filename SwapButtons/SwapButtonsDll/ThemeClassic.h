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
	//virtual SIZE CalcBarOffsets(HWND hWndFrame);
	virtual void PrepareFloatBar(HWND hWndFloatBar);
	virtual void PaintBar(HWND hWndFloatBar, HWND hWndFrame, HDC hDC, const CButtonList& buttonList, RECT rectBar);

	//virtual void PaintStart(HDC hDC, RECT rectBar);
	//virtual void PaintBarBackground(HDC hDC, RECT rectBar);
	//virtual void PaintButtonFace(HDC hDC, RECT rectButton, int index);
	//virtual void PaintButtonSpacing(HDC hDC, RECT rectButton);
	//virtual void PaintBarBorder(HDC hDC, RECT rectBar);
	//virtual void PaintEnd(HDC hDC, RECT rectBar);


private:
	void PaintBarBackground(HDC hDC, RECT rectBar);
	void PaintButtonFace(HDC hDC, RECT rectButton, int index);
	void PaintButtonSpacing(HDC hDC, RECT rectButton);
	void PaintBarBorder(HDC hDC, RECT rectBar);
	void PaintEnd(HDC hDC, RECT rectBar);

private:
	CBitmapBuffer m_BitmapBuffer;

	int m_nButtonWidth;
	int m_nButtonHeight;


private:
	static const int LEFT_BORDER;
	static const int RIGHT_BORDER;
	static const int TOP_BORDER;
	static const int BOTTOM_BORDER;
	static const int BUTTON_SPACING;

};
