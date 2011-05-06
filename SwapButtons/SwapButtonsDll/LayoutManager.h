#pragma once

#include "SwapButtonsDll.h"
#include "ThemeClassic.h"
#include "ThemeBasic.h"
#include "ThemeDwm.h"

class CButtonList;

struct LayoutMetrics
{
	int m_nButtonWidth;
	int m_nButtonHeight;
	int m_nLeftBorder;
	int m_nRightBorder;
	int m_nTopBorder;
	int m_nBottomBorder;
	int m_nSpacing;
};

class LOCALMODE_API CLayoutManager
{
public:
	CLayoutManager(void);
	~CLayoutManager(void);


	void ReInit(HWND hWndFrame);
	void LoadBitmaps(HMODULE hModule);

	void PrepareFloatBar(HWND hWndFloatBar);

	RECT GetBarRect(HWND hWndFrame, const CButtonList& buttonList);
	SIZE GetBarSize(const CButtonList& buttonList);

	void PaintBar(HWND hWndFloatBar, const CButtonList& buttonList);
	//int GetButtonWidth();
	int HitToIndex(int x, int y, const CButtonList& buttonList);


private:
	SIZE CalcBarOffsets(HWND hWndFrame);
	static bool IsPreVista();

private:
	CThemeClassic m_ThemeClassic;
	CThemeBasic m_ThemeBasic;
	CThemeDwm m_ThemeDwm;
	CTheme* m_pCurTheme;
	struct LayoutMetrics m_LayoutMetrics;
};

